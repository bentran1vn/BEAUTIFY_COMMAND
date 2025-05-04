namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorCertificate;
public class CreateDoctorCertificateCommandHandler(
    IRepositoryBase<DOMAIN.Entities.DoctorCertificate, Guid> doctorCertificateRepository,
    IMediaService mediaService,
    IRepositoryBase<Staff, Guid> staffRepository)
    : ICommandHandler<CONTRACT.Services.DoctorCertificate.Commands.CreateDoctorCertificateCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.DoctorCertificate.Commands.CreateDoctorCertificateCommand request,
        CancellationToken cancellationToken)
    {
        var user = await staffRepository.FindByIdAsync(request.DoctorId, cancellationToken);
        if (user == null)
            return Result.Failure(new Error("404", "User Not Found"));
        if (user.Role?.Name != Constant.Role.DOCTOR)
            return Result.Failure(new Error("400", "User is not a doctor"));
        // check if doctor certificate already exist
        var doctorCertificate = await doctorCertificateRepository.FindSingleAsync(
            x => x.CertificateName == request.CertificateName && x.Id == request.DoctorId, cancellationToken);
        if (doctorCertificate != null)
            return Result.Failure(new Error("400", "Doctor certificate already exist"));
        // create new doctor certificate
        var doctor = new DOMAIN.Entities.DoctorCertificate
        {
            CertificateName = request.CertificateName,
            CertificateUrl = await mediaService.UploadImageAsync(request.CertificateFile),
            ExpiryDate = request.ExpiryDate,
            DoctorId = request.DoctorId,
            CategoryId = request.CategoryId,
            Note = request.Note
        };
        doctorCertificateRepository.Add(doctor);
        return Result.Success();
    }
}