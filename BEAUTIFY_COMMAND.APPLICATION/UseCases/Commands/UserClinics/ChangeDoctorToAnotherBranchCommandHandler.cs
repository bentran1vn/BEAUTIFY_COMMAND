namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.UserClinics;
internal sealed class ChangeDoctorToAnotherBranchCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<Clinic, Guid> clinicRepositoryBase,
    IRepositoryBase<UserClinic, Guid> userClinicRepositoryBase)
    : ICommandHandler<CONTRACT.Services.UserClinics.Commands.ChangeDoctorToAnotherBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.UserClinics.Commands.ChangeDoctorToAnotherBranchCommand request,
        CancellationToken cancellationToken)
    {
        var doctor = await staffRepository.FindByIdAsync(request.DoctorId, cancellationToken);
        if (doctor == null)
            return Result.Failure(new Error("404", "Doctor Not Found"));

        var oldClinic = await clinicRepositoryBase.FindByIdAsync(request.OldBranchId, cancellationToken);
        if (oldClinic == null)
            return Result.Failure(new Error("404", "Old Clinic Not Found"));
        var newClinic = await clinicRepositoryBase.FindByIdAsync(request.NewBranchId, cancellationToken);
        if (newClinic == null)
            return Result.Failure(new Error("404", "New Clinic Not Found"));
        var userClinic = await userClinicRepositoryBase.FindSingleAsync(x =>
            x.UserId == request.DoctorId && x.ClinicId == request.OldBranchId, cancellationToken);
        if (userClinic == null)
            return Result.Failure(new Error("404", "User Clinic Not Found"));

        userClinic.ClinicId = request.NewBranchId;
        userClinicRepositoryBase.Update(userClinic);

        return Result.Success();
    }
}