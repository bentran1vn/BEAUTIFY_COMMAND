namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
public class UpdateWorkingScheduleCommandHandler(
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
    IRepositoryBase<User, Guid> userRepository)
    : ICommandHandler<CONTRACT.Services.WorkingSchedules.Commands.UpdateWorkingScheduleCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.WorkingSchedules.Commands.UpdateWorkingScheduleCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Collect all IDs we need to update
        var scheduleIdsToUpdate = request.WorkingDates
            .Select(d => d.WorkingScheduleId)
            .ToList();

        if (scheduleIdsToUpdate.Count == 0) return Result.Failure(new Error("EmptyRequest", "No schedules to update."));

        // 2. Fetch existing schedules from DB
        //    (Adjust this call to match your repository pattern.)
        var existingSchedules = await workingScheduleRepository
            .FindAll(s => scheduleIdsToUpdate.Contains(s.Id)).ToListAsync(cancellationToken);
        var user = await userRepository.FindSingleAsync(x => x.Id.Equals(existingSchedules[0].DoctorClinic!.UserId),
            cancellationToken);
        if (user == null) return Result.Failure(new Error("404", "Doctor not found"));

        var doctorName = $"{user.FirstName} {user.LastName}";
        // 3. Validate all schedules exist
        if (existingSchedules.Count != scheduleIdsToUpdate.Count)
            return Result.Failure(new Error("NotFound",
                "Some of the schedules to update were not found in the database."));

        // 4. [Optional] Ensure all schedules belong to the same doctor or clinic
        //    (Adjust logic if you allow multiple doctors or clinics in a single update.)
        var distinctDoctorClinicIds = existingSchedules
            .Select(s => s.DoctorClinicId)
            .Distinct()
            .ToList();

        if (distinctDoctorClinicIds.Count != 1)
            return Result.Failure(new Error("MultipleDoctorClinics",
                "All schedules to be updated must belong to the same doctor/clinic."));

        var doctorClinicId = distinctDoctorClinicIds.Single();

        // 5. Fetch all other existing schedules for that doctor/clinic (excluding the ones being updated).
        var otherSchedules = await workingScheduleRepository
            .FindAll(s =>
                s.DoctorClinicId == doctorClinicId &&
                !scheduleIdsToUpdate.Contains(s.Id)
            ).ToListAsync(cancellationToken);

        // 6. Prepare an in-memory copy of the schedules to update (for overlap checks within the request)
        var schedulesToUpdate = existingSchedules.ToDictionary(s => s.Id, s => s);

        // 7. Validate & apply updates
        foreach (var updateReq in request.WorkingDates)
        {
            var scheduleId = updateReq.WorkingScheduleId;
            if (!schedulesToUpdate.TryGetValue(scheduleId, out var scheduleEntity))
                return Result.Failure(new Error("NotFound",
                    $"Working schedule with ID {scheduleId} not found."));

            // 7a. Parse new date/time
            var date = DateOnly.Parse(updateReq.Date);
            var startTime = TimeSpan.Parse(updateReq.StartTime);
            var endTime = TimeSpan.Parse(updateReq.EndTime);

            // 7b. Basic check: start < end
            if (startTime >= endTime)
                return Result.Failure(new Error("InvalidTimeRange",
                    $"StartTime ({startTime}) must be earlier than EndTime ({endTime})."));

            // 7c. Check for overlap with "otherSchedules" from DB
            var overlapsWithExisting = otherSchedules.Any(s =>
                s.Date == date && TimesOverlap(s.StartTime, s.EndTime, startTime, endTime));

            if (overlapsWithExisting)
                return Result.Failure(new Error("Overlap",
                    $"Cannot update schedule ({startTime} - {endTime}) on {date}. " +
                    "It overlaps with an existing schedule in the database."));

            // 7d. Check for overlap with other updated schedules in this request
            //     We only check among the newly requested updates, not including the current one
            var otherUpdates = schedulesToUpdate
                .Where(x => x.Key != scheduleId) // exclude current
                .Select(x => x.Value);

            var overlapsWithNew = otherUpdates.Any(s =>
                s.Date == date && TimesOverlap(s.StartTime, s.EndTime, startTime, endTime));

            if (overlapsWithNew)
                return Result.Failure(new Error("Overlap",
                    $"Cannot update schedule ({startTime} - {endTime}) on {date}. " +
                    "It overlaps with another schedule in this update request."));

            // 7e. If all checks pass, update in memory
            scheduleEntity.Date = date;
            scheduleEntity.StartTime = startTime;
            scheduleEntity.EndTime = endTime;
        }

        var work = schedulesToUpdate.Values.ToList();
        work[0].WorkingScheduleUpdate(work, doctorName);
        workingScheduleRepository.UpdateRange(schedulesToUpdate.Values);


        return Result.Success();
    }

    /// <summary>
    ///     Utility method to check if two time ranges overlap.
    /// </summary>
    private static bool TimesOverlap(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
    {
        // Two ranges [start1, end1) and [start2, end2) overlap if
        // start1 < end2 AND start2 < end1
        return start1 < end2 && start2 < end1;
    }
}