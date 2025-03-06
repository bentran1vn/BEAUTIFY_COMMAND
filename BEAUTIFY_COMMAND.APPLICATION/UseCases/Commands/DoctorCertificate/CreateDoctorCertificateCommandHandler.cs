using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.DoctorCertificate;
public class CreateDoctorCertificateCommandHandler(
    IRepositoryBase<DOMAIN.Entities.DoctorCertificate, Guid> doctorCertificateRepository,
    IMediaService mediaService,
    IRepositoryBase<User, Guid> userRepository

    // ICurrentUserService currentUserService
)
    : ICommandHandler<CONTRACT.Services.DoctorCertificate.Commands.CreateDoctorCertificateCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.DoctorCertificate.Commands.CreateDoctorCertificateCommand request,
        CancellationToken cancellationToken)
    {
        /* var UserId =
             currentUserService.UserId ?? throw new UnauthorizedAccessException(); //check if user has role doctor
         //check if user has role doctor
         var user = await userRepository.FindByIdAsync(userId, cancellationToken);*/
        var user = await userRepository.FindSingleAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new UnauthorizedAccessException();
        if (user.Role?.Name != Constant.Role.DOCTOR)
            throw new UnauthorizedAccessException();
        // check if doctor certificate already exist
        var doctorCertificate = await doctorCertificateRepository.FindSingleAsync(
            x => x.CertificateName == request.CertificateName && x.Id == request.UserId, cancellationToken);
        if (doctorCertificate != null)
            return Result.Failure(new Error("400", "Doctor certificate already exist"));
        // create new doctor certificate
        var doctor = new DOMAIN.Entities.DoctorCertificate
        {
            CertificateName = request.CertificateName,
            CertificateUrl = await mediaService.UploadImageAsync(request.CertificateFile),
            ExpiryDate = request.ExpiryDate,
            DoctorId = request.UserId,
            Note = request.Note,
        };
        doctorCertificateRepository.Add(doctor);
        return Result.Success();
    }
}