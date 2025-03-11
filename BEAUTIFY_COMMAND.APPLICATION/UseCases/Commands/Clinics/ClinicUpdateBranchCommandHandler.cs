using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class
    ClinicUpdateBranchCommandHandler(IRepositoryBase<Clinic, Guid> clinicRepository, IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicUpdateBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicUpdateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var clinic = await clinicRepository.FindByIdAsync(request.BranchId, cancellationToken) ??
                     throw new ClinicException.ClinicNotFoundException(request.BranchId);
        if (request.ProfilePicture != null)
        {
            var profilePictureUrl = await mediaService.UploadImageAsync(request.ProfilePicture);
            clinic.ProfilePictureUrl = profilePictureUrl;
        }

        clinic.Name = request.Name;
        clinic.City = request.City;
        clinic.District = request.District;
        clinic.Ward = request.Ward;
        clinic.HouseNumber = request.HouseNumber;
        clinic.PhoneNumber = request.PhoneNumber;
        clinic.IsActivated = request.IsActivated;
        clinicRepository.Update(clinic);
        return Result.Success();
    }
}