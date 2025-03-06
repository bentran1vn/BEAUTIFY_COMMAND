using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
internal sealed class CreateWorkingScheduleCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
    IRepositoryBase<Clinic, Guid> clinicRepository)
    : ICommandHandler<CONTRACT.Services.WorkingSchedules.Commands.CreateWorkingScheduleCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.WorkingSchedules.Commands.CreateWorkingScheduleCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validate doctor
        var doctor = await userRepository.FindByIdAsync(request.DoctorId, cancellationToken)
                     ?? throw new UserException.UserNotFoundException(request.DoctorId);
        var doctorName = $"{doctor.FirstName} {doctor.LastName}";
        if (doctor.UserClinics == null || doctor.UserClinics.Count == 0)
        {
            throw new UserClinicException.UserClinicNotFoundException();
        }

        if (doctor.Role?.Name != Constant.Role.DOCTOR)
        {
            return Result.Failure(new Error("403", "User is not a doctor"));
        }

        // 2. Validate clinic
        var doctorClinic = doctor.UserClinics.FirstOrDefault();
        var clinic = await clinicRepository.FindByIdAsync(doctorClinic!.ClinicId, cancellationToken)
                     ?? throw new ClinicException.ClinicNotFoundException();

        // This list will hold the final schedules to be added
        var schedulesToAdd = new List<WorkingSchedule>();

        // 3. Process each request date/time
        foreach (var workingDateRequest in request.WorkingDates)
        {
            var date = DateOnly.Parse(workingDateRequest.Date);
            var startTime = TimeSpan.Parse(workingDateRequest.StartTime);
            var endTime = TimeSpan.Parse(workingDateRequest.EndTime);

            // 3a. Basic time validation
            if (startTime >= endTime)
            {
                return Result.Failure(new Error("InvalidTimeRange",
                    $"StartTime ({startTime}) must be earlier than EndTime ({endTime})."));
            }

            // 3b. Check overlap with existing schedules in DB
            //     (Assuming you have a FindAsync/GetListAsync or similar method).
            var existingSchedulesOnDate = await workingScheduleRepository
                .FindAll(x => x.DoctorClinicId == doctorClinic.Id && x.Date == date).ToListAsync(cancellationToken);

            var overlapsWithExisting = existingSchedulesOnDate.Any(x =>
                TimesOverlap(x.StartTime, x.EndTime, startTime, endTime));

            if (overlapsWithExisting)
            {
                return Result.Failure(new Error("Overlap",
                    $"Cannot add schedule ({startTime} - {endTime}) on {date}. It overlaps with an existing schedule."));
            }

            // 3c. Check overlap within the *new* schedules in this request
            var overlapsWithNew = schedulesToAdd.Any(x =>
                x.Date == date && TimesOverlap(x.StartTime, x.EndTime, startTime, endTime));

            if (overlapsWithNew)
            {
                return Result.Failure(new Error("Overlap",
                    $"Cannot add schedule ({startTime} - {endTime}) on {date}. It overlaps with another schedule in this request."));
            }

            // 3d. All good – add to the list
            schedulesToAdd.Add(new WorkingSchedule
            {
                Id = Guid.NewGuid(),
                DoctorClinicId = doctorClinic.Id,
                Date = date,
                StartTime = startTime,
                EndTime = endTime
            });
        }

        // 4. If we have no schedules to add, return
        if (schedulesToAdd.Count == 0)
        {
            return Result.Failure(new Error("NoValidSchedules", "No valid schedules to add."));
        }

        schedulesToAdd[0].WorkingScheduleCreate(doctor.Id, clinic.Id, doctorName, schedulesToAdd);

        workingScheduleRepository.AddRange(schedulesToAdd);
        // Save changes if your repository requires an explicit save:
        // await workingScheduleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Utility method to check if two time ranges overlap.
    /// </summary>
    private static bool TimesOverlap(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
    {
        // Two ranges [start1, end1) and [start2, end2) overlap if
        // start1 < end2 AND start2 < end1
        return start1 < end2 && start2 < end1;
    }
}