namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
internal sealed class DoctorRegisterScheduleCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
    IRepositoryBase<UserClinic, Guid> userClinicRepository,
    IRepositoryBase<Config, Guid> configRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.WorkingSchedules.Commands.DoctorRegisterScheduleCommand>
{
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
                 x.ClinicId == request.clinicId,
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
        if (requestedSchedules.Any(x => x.DoctorId != null))
            return Result.Failure(new Error("409", "Một hoặc nhiều lịch làm việc đã được đăng ký cho bác sĩ khác"));

        // 5. Calculate total hours for the requested schedules
        var requestedHours = requestedSchedules.Sum(x => (x.EndTime - x.StartTime).TotalHours);

        // 6. Get doctor's existing schedules for the week
        var requestedWeekStart = GetWeekStartDate(requestedSchedules.Min(x => x.Date));
        var requestedWeekEnd = requestedWeekStart.AddDays(6);

        var existingSchedules = await workingScheduleRepository
            .FindAll(x => x.DoctorId == doctorClinic.User.Id &&
                          x.Date >= requestedWeekStart &&
                          x.Date <= requestedWeekEnd)
            .ToListAsync(cancellationToken);

        // 4.5. Check for overlaps within requested schedules
        for (var i = 0; i < requestedSchedules.Count; i++)
        {
            var schedule1 = requestedSchedules[i];

            for (var j = i + 1; j < requestedSchedules.Count; j++)
            {
                var schedule2 = requestedSchedules[j];

                // Check if schedules are on the same date
                if (schedule1.Date != schedule2.Date) continue;
                // Check for time overlap
                if (TimesOverlap(schedule1.StartTime, schedule1.EndTime, schedule2.StartTime, schedule2.EndTime))
                {
                    return Result.Failure(new Error("409",
                        $"Lịch làm việc từ {schedule1.StartTime} đến {schedule1.EndTime} trùng thời gian với lịch từ {schedule2.StartTime} đến {schedule2.EndTime} vào ngày {schedule1.Date}"));
                }
            }
        }

        // Check for overlaps with doctor's existing schedules
        foreach (var newSchedule in requestedSchedules)
        {
            foreach (var existingSchedule in existingSchedules.Where(existingSchedule =>
                         newSchedule.Date == existingSchedule.Date &&
                         TimesOverlap(newSchedule.StartTime, newSchedule.EndTime, existingSchedule.StartTime,
                             existingSchedule.EndTime)))
            {
                return Result.Failure(new Error("409",
                    $"Lịch làm việc từ {newSchedule.StartTime} đến {newSchedule.EndTime} vào ngày {newSchedule.Date} trùng thời gian với lịch hiện tại từ {existingSchedule.StartTime} đến {existingSchedule.EndTime}"));
            }
        }

        var existingHours = existingSchedules.Sum(x => (x.EndTime - x.StartTime).TotalHours);
        var config = await configRepository
            .FindSingleAsync(x => x.Key == "Maximun_Working_Hours_A_Week", cancellationToken);

        if (config == null)
            return Result.Failure(new Error("404", "Không tìm thấy cấu hình giới hạn giờ làm việc"));
        if (!int.TryParse(config.Value, out var Maximun_Working_Hours_A_Week))
        {
            return Result.Failure(new Error("422", "Giá trị giới hạn giờ làm việc không hợp lệ"));
        }

        // 7. Check if adding the new schedules would exceed the weekly limit
        if (existingHours + requestedHours > Maximun_Working_Hours_A_Week)
            return Result.Failure(new Error("422",
                $"Không thể đăng kí những lịch làm việc này. Vì đã vượt quá giới hạn {Maximun_Working_Hours_A_Week} giờ 1 tuần. " +
                $"Hiện tại: {existingHours} giờ, đăng ký: {requestedHours} giờ"));

        // 8. Assign the doctor to the schedules
        foreach (var schedule in requestedSchedules)
        {
            schedule.DoctorId = doctorClinic.User.Id;
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

    private static bool TimesOverlap(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
    {
        // Two ranges [start1, end1) and [start2, end2) overlap if
        // start1 < end2 AND start2 < end1
        return start1 < end2 && start2 < end1;
    }
}