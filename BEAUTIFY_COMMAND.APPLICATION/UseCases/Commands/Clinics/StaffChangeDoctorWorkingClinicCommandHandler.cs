using BEAUTIFY_COMMAND.DOMAIN.Exceptions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class StaffChangeDoctorWorkingClinicCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<UserClinic, Guid> userClinicRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.StaffChangeDoctorWorkingClinicCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.StaffChangeDoctorWorkingClinicCommand request,
        CancellationToken cancellationToken)
    {
        var user = await staffRepository.FindByIdAsync(request.DoctorId, cancellationToken) ??
                   throw new UserException.UserNotFoundException(request.DoctorId);
        var clinic = await clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken) ??
                     throw new ClinicException.ClinicNotFoundException(request.ClinicId);

        if (!clinic.IsActivated) return Result.Failure(new Error("400", "Clinic is not activated"));

        if (user.UserClinics != null && user.UserClinics.Any(x => x.ClinicId == request.ClinicId))
            return Result.Failure(new Error("400", "Doctor already working in this clinic"));

        var userClinic =
            await userClinicRepository.FindSingleAsync(x => x.UserId == request.DoctorId, cancellationToken)
            ?? throw new UserException.UserNotFoundException(request.DoctorId);
        userClinic.ClinicId = request.ClinicId;
        userClinicRepository.Update(userClinic);

        return Result.Success();
    }
}