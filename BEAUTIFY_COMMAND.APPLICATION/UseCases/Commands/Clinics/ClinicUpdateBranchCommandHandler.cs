﻿namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class
    ClinicUpdateBranchCommandHandler(
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerRepository,
        IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicUpdateBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicUpdateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var clinic =
            await clinicRepository.FindSingleAsync(x => x.Id.Equals(request.BranchId) && !x.IsDeleted,
                cancellationToken) ??
            throw new ClinicException.ClinicNotFoundException(request.BranchId);
        
        var clinicPhone = await clinicRepository
            .FindAll(x => x.PhoneNumber.Equals(request.PhoneNumber))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(clinicPhone != null && clinicPhone.Id != request.BranchId)
            return Result.Failure(new Error("400", "Phone number already exists"));
        
        if (request.ProfilePicture != null)
        {
            var profilePictureUrl = await mediaService.UploadImageAsync(request.ProfilePicture);
            clinic.ProfilePictureUrl = profilePictureUrl;
        }

        if (request.OperatingLicense != null)
        {
            var operatingLicenseUrl = await mediaService.UploadImageAsync(request.OperatingLicense);
            clinic.OperatingLicenseUrl = operatingLicenseUrl;
        }

        if (request.BusinessLicense != null)
        {
            var businessLicenseUrl = await mediaService.UploadImageAsync(request.BusinessLicense);
            clinic.BusinessLicenseUrl = businessLicenseUrl;
        }

        if (request.BankAccountNumber != null)
        {
            clinic.BankAccountNumber = request.BankAccountNumber;
        }

        if (request.BankName != null)
        {
            clinic.BankName = request.BankName;
        }
        
        clinic.OperatingLicenseExpiryDate = request.OperatingLicenseExpiryDate;
        clinic.Name = request.Name;
        clinic.City = request.City;
        clinic.District = request.District;
        clinic.Ward = request.Ward;
        clinic.Address = request.Address;
        clinic.WorkingTimeStart = request.WorkingTimeStart;
        clinic.WorkingTimeEnd = request.WorkingTimeEnd;
        clinic.PhoneNumber = request.PhoneNumber;
        clinic.IsActivated = request.IsActivated;
        clinicRepository.Update(clinic);
        
        var trigger = TriggerOutbox.UpdateBranchEvent(false, clinic);
        triggerRepository.Add(trigger);
        
        return Result.Success();
    }
}