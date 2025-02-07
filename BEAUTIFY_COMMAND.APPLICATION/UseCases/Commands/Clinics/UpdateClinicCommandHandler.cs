using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;


namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;

public class UpdateClinicCommandHandler : ICommandHandler<CONTRACT.Services.Clinics.Commands.UpdateClinicCommand>
{
    private readonly IRepositoryBase<DOMAIN.Entities.Clinics, Guid> _clinicRepository;
    private readonly IMediaService _mediaService;

    public UpdateClinicCommandHandler(IRepositoryBase<DOMAIN.Entities.Clinics, Guid> clinicRepository, IMediaService mediaService)
    {
        _clinicRepository = clinicRepository;
        _mediaService = mediaService;
    }

    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.UpdateClinicCommand request, CancellationToken cancellationToken)
    {
        var clinic = await _clinicRepository.FindByIdAsync(new Guid(request.ClinicId), cancellationToken);

        if (clinic == null || clinic.IsDeleted)
        {
            return Result.Failure(new Error("404", "Clinic not found."));
        }

        if (request.Name != null)
        {
            clinic.Name = request.Name;
        }

        if (request.PhoneNumber != null)
        {
            clinic.PhoneNumber = request.PhoneNumber;
        }

        if (request.Address != null)
        {
            clinic.Address = request.Address;
        }

        if (request.ProfilePicture != null)
        {
            var url = await _mediaService.UploadImageAsync(request.ProfilePicture);
            clinic.ProfilePictureUrl = url;
        }

        if (request.IsActivated != null)
        {
            clinic.IsActivated = request.IsActivated.Value;
        }
        
        return Result.Success("Clinic updated.");
    }
}