﻿namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class ClinicDeleteAccountOfEmployeeCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    ICurrentUserService currentUserService,
    IRepositoryBase<UserClinic, Guid> userClinicRepository,
    IRepositoryBase<WorkingSchedule, Guid> workingScheduleRepository,
    IRepositoryBase<DoctorService, Guid> doctorServiceRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicDeleteAccountOfEmployeeCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicDeleteAccountOfEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var user = await staffRepository.FindByIdAsync(request.UserId, cancellationToken) ??
                   throw new UserException.UserNotFoundException(request.UserId);
        if (user?.Role?.Name == Constant.Role.CLINIC_ADMIN) throw new UnauthorizedAccessException();

        // Get the user's clinic directly from their UserClinics record
        var userClinic = user.UserClinics?.FirstOrDefault() ??
                         throw new UserClinicException.UserClinicNotFoundException();

        // Check if the user has any future working schedules
        if (userClinic != null)
        {
            // Get current date in Vietnam timezone
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var currentDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);
            var today = DateOnly.FromDateTime(currentDate.DateTime);

            // Check if any future working schedules exist (more efficient than loading all records)
            var hasFutureSchedules = await workingScheduleRepository
                .FindAll(x => x.DoctorId == user.Id && x.ClinicId == currentUserService.ClinicId && x.Date >= today)
                .AnyAsync(cancellationToken);

            if (hasFutureSchedules)
            {
                // Get the next scheduled date for a more informative error message
                var nextSchedule = await workingScheduleRepository
                    .FindAll(x => x.DoctorId == user.Id && x.ClinicId == currentUserService.ClinicId && x.Date >= today)
                    .OrderBy(x => x.Date)
                    .ThenBy(x => x.StartTime)
                    .FirstOrDefaultAsync(cancellationToken);

                var errorMessage = nextSchedule != null
                    ? $"Cannot delete employee account. The user has a working schedule on {nextSchedule.Date.ToString("yyyy-MM-dd")} at {nextSchedule.StartTime}."
                    : "Cannot delete employee account. The user has future working schedules.";

                return Result.Failure(new Error("400", errorMessage));
            }
        }

        staffRepository.Remove(user);
        userClinicRepository.Remove(userClinic);


        var doctorService = await doctorServiceRepository.FindAll(x =>
                x.DoctorId == request.UserId &&
                x.Service.ClinicServices.FirstOrDefault().ClinicId == userClinic.ClinicId)
            .ToListAsync(cancellationToken);
        if (doctorService.Count != 0) doctorServiceRepository.RemoveMultiple(doctorService);

        userClinic.RaiseDoctorFromClinicDeletedEvent(user.Id);
        return Result.Success();
    }
}