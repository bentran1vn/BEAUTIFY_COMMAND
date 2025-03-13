using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;


namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class UpdateClinicCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.UpdateClinicCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.UpdateClinicCommand request,
        CancellationToken cancellationToken)
    {
        var clinic = await clinicRepository.FindByIdAsync(new Guid(request.ClinicId), cancellationToken);

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

        if (request.City != null)
        {
            clinic.City = request.City;
        }

        if (request.District != null)
        {
            clinic.District = request.District;
        }

        if (request.Ward != null)
        {
            clinic.Ward = request.Ward;
        }

        if (request.Address != null)
        {
            clinic.Address = request.Address;
        }

        if (request.ProfilePicture != null)
        {
            var url = await mediaService.UploadImageAsync(request.ProfilePicture);
            clinic.ProfilePictureUrl = url;
        }

        if (request.IsActivated != null)
        {
            clinic.IsActivated = request.IsActivated.Value;
        }
        if (request.BankName != null)
        {
            clinic.BankName = request.BankName;
        }
        if (request.BankAccountNumber != null)
        {
            clinic.BankAccountNumber = request.BankAccountNumber;
        }

        return Result.Success("Clinic updated.");
    }
}