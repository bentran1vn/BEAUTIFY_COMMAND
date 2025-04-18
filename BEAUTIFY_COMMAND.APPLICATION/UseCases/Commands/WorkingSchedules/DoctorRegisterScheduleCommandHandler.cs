namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
internal sealed class DoctorRegisterScheduleCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
    IRepositoryBase<UserClinic, Guid> userClinicRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.WorkingSchedules.Commands.DoctorRegisterScheduleCommand>
{
    // Maximum weekly working hours (44 hours)
    private const int MaxWeeklyHours = 44;

    public async Task<Result> Handle(CONTRACT.Services.WorkingSchedules.Commands.DoctorRegisterScheduleCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validate doctor
        var doctor = await staffRepository.FindByIdAsync(currentUserService.UserId.Value, cancellationToken);
        if (doctor == null)
            return Result.Failure(new Error("404", "Không tìm thấy bác sĩ"));


        var doctorName = $"{doctor.FirstName} {doctor.LastName}";


        // 2. Get doctor's clinic association
        var doctorClinic = await userClinicRepository.FindSingleAsync(
            x => x.UserId == currentUserService.UserId.Value &&
                 x.ClinicId == currentUserService.ClinicId.Value,
            cancellationToken);

        if (doctorClinic == null)
            return Result.Failure(new Error("404", "Bác sĩ không thuộc phòng khám này"));

        // 3. Get the requested schedules
        var requestedSchedules = await workingScheduleRepository
            .FindAll(x => request.WorkingScheduleIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (requestedSchedules.Count != request.WorkingScheduleIds.Count)
            return Result.Failure(new Error("404", "Một hoặc nhiều lịch làm việc không tồn tại"));

        // 4. Validate that all schedules are empty (not assigned to any doctor)
        if (requestedSchedules.Any(x => x.DoctorClinicId != null))
            return Result.Failure(new Error("409", "Một hoặc nhiều lịch làm việc đã được đăng ký cho bác sĩ khác"));

        // 5. Calculate total hours for the requested schedules
        var requestedHours = requestedSchedules.Sum(x => (x.EndTime - x.StartTime).TotalHours);

        // 6. Get doctor's existing schedules for the week
        var requestedWeekStart = GetWeekStartDate(requestedSchedules.Min(x => x.Date));
        var requestedWeekEnd = requestedWeekStart.AddDays(6);

        var existingSchedules = await workingScheduleRepository
            .FindAll(x => x.DoctorClinicId == doctorClinic.Id &&
                          x.Date >= requestedWeekStart &&
                          x.Date <= requestedWeekEnd)
            .ToListAsync(cancellationToken);

        var existingHours = existingSchedules.Sum(x => (x.EndTime - x.StartTime).TotalHours);

        // 7. Check if adding the new schedules would exceed the weekly limit
        if (existingHours + requestedHours > MaxWeeklyHours)
            return Result.Failure(new Error("422",
                $"Không thể đăng kí những lịch làm việc này. Vì đã vượt quá giới hạn {MaxWeeklyHours} giờ 1 tuần. " +
                $"Hiện tại: {existingHours} giờ, đăng ký: {requestedHours} giờ"));

        // 8. Assign the doctor to the schedules
        foreach (var schedule in requestedSchedules)
        {
            schedule.DoctorClinicId = doctorClinic.Id;
            workingScheduleRepository.Update(schedule);
        }

        // 9. Raise domain event
        if (requestedSchedules.Count != 0)
        {
            requestedSchedules[0]
                .RegisterDoctorSchedule(currentUserService.UserId.Value, doctorName, requestedSchedules);
        }

        return Result.Success();
    }

    private static DateOnly GetWeekStartDate(DateOnly date)
    {
        // Get the start of the week (Monday) for the given date
        var diff = (int)date.DayOfWeek - 1;
        if (diff < 0) diff += 7; // Adjust for Sunday (DayOfWeek = 0)
        return date.AddDays(-diff);
    }
}