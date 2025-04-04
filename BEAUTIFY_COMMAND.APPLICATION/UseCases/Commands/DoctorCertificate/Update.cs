namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorCertificate;
internal sealed class Update(
    IRepositoryBase<DOMAIN.Entities.DoctorCertificate, Guid> _repositoryBase,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.DoctorCertificate.Commands.UpdateCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.DoctorCertificate.Commands.UpdateCommand request,
        CancellationToken cancellationToken)
    {
        var doctorCertificate = await _repositoryBase.FindSingleAsync(x => x.Id == request.Id, cancellationToken);
        if (doctorCertificate == null)
            return Result.Failure(new Error("400", "Doctor certificate not found"));
        doctorCertificate.CertificateName = request.CertificateName;
        doctorCertificate.ExpiryDate = request.ExpiryDate;
        doctorCertificate.Note = request.Note;
        if (request.CertificateFile != null)
        {
            var url = await mediaService.UploadImageAsync(request.CertificateFile);
            doctorCertificate.CertificateUrl = url;
        }

        _repositoryBase.Update(doctorCertificate);
        return Result.Success();
    }
}