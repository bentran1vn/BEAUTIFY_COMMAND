using BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Repositories;
using Microsoft.EntityFrameworkCore;
using r = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Shared.Result;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.WorkingSchedules;
internal sealed class DoctorRegisterScheduleCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
    IRepositoryBase<UserClinic, Guid> userClinicRepository)
    : ICommandHandler<Commands.DoctorRegisterScheduleCommand>
{
    // Maximum weekly working hours (44 hours)
    private const int MaxWeeklyHours = 44;
    
    public async Task<r> Handle(Commands.DoctorRegisterScheduleCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Validate doctor
        var doctor = await staffRepository.FindByIdAsync(request.DoctorId, cancellationToken)
                     ?? throw new UserException.UserNotFoundException(request.DoctorId);
        
        var doctorName = $"{doctor.FirstName} {doctor.LastName}";
        
        if (doctor.Role?.Name != Constant.Role.DOCTOR)
            return Result.Failure(new Error("403", "User is not a doctor"));
        
        // 2. Get doctor's clinic association
        var doctorClinic = await userClinicRepository.FindSingleAsync(
            x => x.UserId == request.DoctorId, 
            cancellationToken);
        
        if (doctorClinic == null)
            return Result.Failure(new Error("404", "Doctor is not associated with any clinic"));
        
        // 3. Get the requested schedules
        var requestedSchedules = await workingScheduleRepository
            .FindAll(x => request.WorkingScheduleIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
        
        if (requestedSchedules.Count != request.WorkingScheduleIds.Count)
            return Result.Failure(new Error("404", "One or more requested schedules not found"));
        
        // 4. Validate that all schedules are empty (not assigned to any doctor)
        if (requestedSchedules.Any(x => x.DoctorClinicId != null))
            return Result.Failure(new Error("409", "One or more schedules are already assigned to a doctor"));
        
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
                $"Cannot register for these schedules. It would exceed the maximum weekly working hours of {MaxWeeklyHours} hours. " +
                $"Current: {existingHours} hours, Requested: {requestedHours} hours"));
        
        // 8. Assign the doctor to the schedules
        foreach (var schedule in requestedSchedules)
        {
            schedule.DoctorClinicId = doctorClinic.Id;
            workingScheduleRepository.Update(schedule);
        }
        
        // 9. Raise domain event
        if (requestedSchedules.Any())
        {
            requestedSchedules[0].RegisterDoctorSchedule(request.DoctorId, doctorName, requestedSchedules);
        }
        
        return Result.Success();
    }
    
    private static DateOnly GetWeekStartDate(DateOnly date)
    {
        // Get the start of the week (Monday) for the given date
        int diff = (int)date.DayOfWeek - 1;
        if (diff < 0) diff += 7; // Adjust for Sunday (DayOfWeek = 0)
        return date.AddDays(-diff);
    }
}
