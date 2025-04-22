using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
internal sealed class CreateClinicEmptyScheduleCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
    IRepositoryBase<ShiftConfig, Guid> shiftConfigRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.WorkingSchedules.Commands.CreateClinicEmptyScheduleCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.WorkingSchedules.Commands.CreateClinicEmptyScheduleCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validate clinic
        var clinic = await clinicRepository.FindByIdAsync(currentUserService.ClinicId.Value, cancellationToken)
                     ?? throw new ClinicException.ClinicNotFoundException();

        // Ensure clinic has working hours defined
        if (clinic.WorkingTimeStart == null || clinic.WorkingTimeEnd == null)
            return Result.Failure(new Error("400",
                ErrorMessages.Clinic.ClinicDoNotHaveWorkingHours));

        var clinicName = clinic.Name;

        // This list will hold the final schedules to be added
        var schedulesToAdd = new List<WorkingSchedule>();

        // 2. Process each request date/time
        foreach (var workingDateRequest in request.WorkingDates)
        {
            var shiftConfig =
                await shiftConfigRepository.FindByIdAsync(workingDateRequest.ShiftGroupId, cancellationToken);
            if (shiftConfig == null)
                return Result.Failure(new Error("404",
                    ErrorMessages.ShiftConfig.ShiftConfigNotFound));
            var date = workingDateRequest.Date;

            var capacity = workingDateRequest.Capacity;

            // 2a. Basic time validation
            if (shiftConfig.StartTime >= shiftConfig.EndTime && shiftConfig.EndTime != TimeSpan.Zero)
                return Result.Failure(new Error("400",
                    ErrorMessages.Clinic.ClinicStartTimeMustBeEarlierThanEndTime(shiftConfig.StartTime,
                        shiftConfig.EndTime)));

            // 2b. Validate time is within clinic working hours
            if (shiftConfig.StartTime < clinic.WorkingTimeStart || shiftConfig.EndTime > clinic.WorkingTimeEnd)
                return Result.Failure(new Error("400",
                    ErrorMessages.Clinic.OutsideWorkingHours(shiftConfig.StartTime, shiftConfig.EndTime,
                        clinic.WorkingTimeStart,
                        clinic.WorkingTimeEnd)));

            // 2c. Check for existing shift groups with the same time range
            var existingShiftGroups = await workingScheduleRepository
                .FindAll(x => x.Date == date &&
                              x.StartTime == shiftConfig.StartTime &&
                              x.EndTime == shiftConfig.EndTime &&
                              x.ShiftGroupId != null)
                .ToListAsync(cancellationToken);

            Guid shiftGroupId;

            // If a shift group already exists for this time slot
            if (existingShiftGroups.Count != 0)
            {
                var existingGroup = existingShiftGroups.First();
                shiftGroupId = existingGroup.ShiftGroupId.Value;

                // Count how many slots are already filled
                var filledSlots = existingShiftGroups.Count(x => x.DoctorId != null);
                var currentCapacity = existingGroup.ShiftCapacity ?? 1;

                // If we're trying to increase capacity, we need to add more slots
                if (capacity > currentCapacity)
                {
                    // Update existing slots with new capacity
                    foreach (var slot in existingShiftGroups)
                    {
                        slot.ShiftCapacity = capacity;
                        workingScheduleRepository.Update(slot);
                    }

                    // Add additional empty slots
                    for (var i = 0; i < capacity - currentCapacity; i++)
                    {
                        schedulesToAdd.Add(new WorkingSchedule
                        {
                            Id = Guid.NewGuid(),
                            Date = date,
                            StartTime = shiftConfig.StartTime,
                            EndTime = shiftConfig.EndTime,
                            ShiftGroupId = shiftGroupId,
                            ClinicId = currentUserService.ClinicId.Value,
                            ShiftCapacity = capacity
                        });
                    }

                    // Raise domain event for capacity change
                    if (existingShiftGroups.Count == 0) continue;
                    var allAffectedSchedules = new List<WorkingSchedule>(existingShiftGroups);
                    allAffectedSchedules.AddRange(schedulesToAdd);
                    existingShiftGroups[0].ChangeShiftCapacity(
                        clinic.Id,
                        clinicName,
                        shiftGroupId,
                        currentCapacity,
                        capacity,
                        allAffectedSchedules);
                }
                // If we're trying to decrease capacity, check if it's possible
                else if (capacity < currentCapacity)
                {
                    if (filledSlots > capacity)
                    {
                        return Result.Failure(new Error("CannotReduceCapacity",
                            $"Cannot reduce capacity to {capacity} as there are already {filledSlots} doctors assigned to this shift."));
                    }

                    // Update existing slots with new capacity
                    foreach (var slot in existingShiftGroups)
                    {
                        slot.ShiftCapacity = capacity;
                        workingScheduleRepository.Update(slot);
                    }

                    // Remove excess empty slots
                    var emptySlots = existingShiftGroups.Where(x => x.DoctorId == null).ToList();
                    var slotsToRemove = new List<WorkingSchedule>();

                    for (var i = 0; i < currentCapacity - capacity; i++)
                    {
                        if (i >= emptySlots.Count) continue;
                        slotsToRemove.Add(emptySlots[i]);
                        workingScheduleRepository.Remove(emptySlots[i]);
                    }

                    // Raise domain event for capacity change
                    if (existingShiftGroups.Count == 0) continue;
                    var remainingSchedules = existingShiftGroups.Except(slotsToRemove).ToList();
                    if (remainingSchedules.Count != 0)
                    {
                        remainingSchedules[0].ChangeShiftCapacity(
                            clinic.Id,
                            clinicName,
                            shiftGroupId,
                            currentCapacity,
                            capacity,
                            remainingSchedules);
                    }
                }
                // If capacity is the same, no action needed
                else if (capacity == currentCapacity)
                {
                    // No changes needed
                }
            }
            else
            {
                // Create a new shift group using ShiftConfig's Id instead of generating a new GUID
                shiftGroupId = shiftConfig.Id;

                // Create empty slots based on capacity
                for (var i = 0; i < capacity; i++)
                {
                    schedulesToAdd.Add(new WorkingSchedule
                    {
                        Id = Guid.NewGuid(),
                        Date = date,
                        StartTime = shiftConfig.StartTime,
                        ClinicId = currentUserService.ClinicId.Value,
                        EndTime = shiftConfig.EndTime,
                        ShiftGroupId = shiftGroupId,
                        ShiftCapacity = capacity
                    });
                }
            }
        }

        // 3. If we have schedules to add, create them
        if (schedulesToAdd.Count <= 0) return Result.Success();
        schedulesToAdd[0].CreateEmptyClinicSchedule(clinic.Id, clinicName, schedulesToAdd);
        workingScheduleRepository.AddRange(schedulesToAdd);

        return Result.Success();
    }
}