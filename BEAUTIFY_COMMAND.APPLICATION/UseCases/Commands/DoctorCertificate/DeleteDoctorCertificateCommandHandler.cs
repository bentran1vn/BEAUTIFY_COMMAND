namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorCertificate;
internal sealed class DeleteDoctorCertificateCommandHandler(
    IRepositoryBase<DOMAIN.Entities.DoctorCertificate, Guid> _doctorCertificateRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.DoctorCertificate.Commands.DeleteCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.DoctorCertificate.Commands.DeleteCommand request,
        CancellationToken cancellationToken)
    {
        var doctorCertificate =
            await _doctorCertificateRepository.FindSingleAsync(x => x.Id == request.Id, cancellationToken);
        if (doctorCertificate == null)
            return Result.Failure(new Error("400", "Doctor certificate not found"));
        /*  if (doctorCertificate.DoctorId != currentUserService.UserId)
              return Result.Failure(new Error("400", "Unauthorized"));*/
        _doctorCertificateRepository.Remove(doctorCertificate);
        return Result.Success();
    }
}