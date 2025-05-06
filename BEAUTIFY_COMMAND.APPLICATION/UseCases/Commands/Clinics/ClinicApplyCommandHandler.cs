using BEAUTIFY_COMMAND.DOMAIN.MailTemplates;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class ClinicApplyCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<ClinicOnBoardingRequest, Guid> clinicOnBoardingRequestRepository,
    IMailService mailService,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicApplyCommand>
{
    private readonly TimeZoneInfo _vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request,
        CancellationToken cancellationToken)
    {
        // Validate common inputs
        var validationResult = ValidateRequest(request);
        if (validationResult.IsFailure)
            return validationResult;

        // Handle reapplication with existing clinic ID
        if (request is { RoleName: not null, ClinicId: not null })
        {
            return await HandleReapplication(request, cancellationToken);
        }

        // Handle first-time application
        return await HandleFirstTimeApplication(request, cancellationToken);
    }

    private static Result ValidateRequest(CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request)
    {
        // Check for partial clinic ID/role info
        if ((request.RoleName != null || request.ClinicId != null) &&
            request is not { RoleName: not null, ClinicId: not null })
        {
            return Result.Failure(new Error("500", "Must have both RoleName and ClinicId"));
        }

        // Validate required fields for first application
        if (request.RoleName == null && request.ClinicId == null)
        {
            if (request.BusinessLicense == null || request.OperatingLicense == null ||
                request.ProfilePictureUrl == null || request.OperatingLicenseExpiryDate == null)
            {
                return Result.Failure(new Error("500",
                    "Must have BusinessLicense, OperatingLicense and ProfilePictureUrl"));
            }
        }

        // Validate bank info for reapplications
        if (request is { RoleName: not null, ClinicId: not null } &&
            (request.BankName == null || request.BankAccountNumber == null))
        {
            return Result.Failure(new Error("500", "Must have BankName and BankAccountNumber"));
        }

        return Result.Success();
    }

    private async Task<Result> HandleReapplication(
        CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request,
        CancellationToken cancellationToken)
    {
        if (request.RoleName != "Clinic Admin")
        {
            return Result.Failure(new Error("403", "Not Authorized"));
        }

        var clinic = await clinicRepository.FindByIdAsync((Guid)request.ClinicId, cancellationToken);
        if (clinic == null)
        {
            throw new Exception($"Clinic {request.ClinicId} not found");
        }

        if (clinic.Email != request.Email)
        {
            return Result.Failure(new Error("400", "Clinic Email not match"));
        }

        // Update clinic images if provided
        await UpdateClinicImages(clinic, request);

        // Update clinic details
        UpdateClinicDetails(clinic, request);
        clinic.Status = 0;
        clinic.TotalApply += 1;

        // Create onboarding request
        var clinicOnBoardingRequest = CreateOnboardingRequest(clinic.Id);
        clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);

        // Send confirmation email
        _ = mailService.SendMail(ClinicApplyEmailTemplate.GetApplicationRegisteredTemplate(
            request.Email, request.PhoneNumber, request.TaxCode));

        return Result.Success("Clinic Apply Successfully");
    }

    private async Task<Result> HandleFirstTimeApplication(
        CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request,
        CancellationToken cancellationToken)
    {
        // Check for duplicates
        var duplicateFields = await CheckForDuplicates(request, cancellationToken);
        if (duplicateFields.Count > 0)
        {
            return Result.Failure(new Error("400",
                $"The following information already exists: {string.Join(", ", duplicateFields)}"));
        }

        // Check if clinic exists with same info
        var existingClinic = await GetExistingClinic(request, cancellationToken);
        if (existingClinic != null)
        {
            return await HandleExistingClinic(existingClinic, request);
        }

        // Create new clinic
        var clinic = await CreateNewClinic(request);
        clinicRepository.Add(clinic);

        // Create onboarding request
        var clinicOnBoardingRequest = CreateOnboardingRequest(clinic.Id);
        clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);

        // Send confirmation email
        _ = mailService.SendMail(ClinicApplyEmailTemplate.GetApplicationRegisteredTemplate(
            request.Email, request.PhoneNumber, request.TaxCode));

        return Result.Success("Clinic Apply Successfully");
    }

    private async Task<List<string>> CheckForDuplicates(
        CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request,
        CancellationToken cancellationToken)
    {
        var duplicateFields = new List<string>();

        // Get all clinics that match any of the fields in a single query
        var existingClinics = await clinicRepository
            .FindAll(x =>
                (x.Email.Trim().ToLower() == request.Email.Trim().ToLower() ||
                 x.TaxCode == request.TaxCode ||
                 x.PhoneNumber.Trim().ToLower() == request.PhoneNumber.Trim().ToLower()) &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);

        // Check if any field already exists
        if (existingClinics.Any(x => x.Email.Trim().ToLower() == request.Email.Trim().ToLower()))
            duplicateFields.Add("Email");

        if (existingClinics.Any(x => x.TaxCode == request.TaxCode))
            duplicateFields.Add("Tax Code");

        if (existingClinics.Any(x => x.PhoneNumber.Trim().ToLower() == request.PhoneNumber.Trim().ToLower()))
            duplicateFields.Add("Phone Number");

        return duplicateFields;
    }

    private async Task<Clinic?> GetExistingClinic(
        CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request,
        CancellationToken cancellationToken)
    {
        return await clinicRepository
            .FindAll(
                x => x.Email == request.Email &&
                     x.TaxCode == request.TaxCode &&
                     x.PhoneNumber == request.PhoneNumber &&
                     !x.IsDeleted,
                x => x.ClinicOnBoardingRequests!)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    private async Task<Result> HandleExistingClinic(
        Clinic existingClinic,
        CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request)
    {
        switch (existingClinic.Status)
        {
            case 1:
                return Result.Failure(new Error("400", "Clinics already apply successfully!"));
            case 0:
                return Result.Failure(new Error("400", "Clinics Request is handling!"));
            case 3:
                return Result.Failure(new Error("400", "Clinics is banned!"));
        }

        if (existingClinic.ClinicOnBoardingRequests == null || existingClinic.ClinicOnBoardingRequests.Count == 0)
            return Result.Failure(new Error("404", "ClinicOnBoardingRequests Not Exist"));

        existingClinic.TotalApply += 1;

        // Create new onboarding request
        var clinicOnBoardingRequest = CreateOnboardingRequest(existingClinic.Id);
        clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);

        return Result.Success("Clinic Apply Successfully");
    }

    private async Task<Clinic> CreateNewClinic(CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request)
    {
        // Upload all images in parallel
        var uploadPromises = await Task.WhenAll(
            mediaService.UploadImageAsync(request.BusinessLicense),
            mediaService.UploadImageAsync(request.OperatingLicense),
            mediaService.UploadImageAsync(request.ProfilePictureUrl)
        );

        return new Clinic
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            City = request.City,
            Ward = request.Ward,
            District = request.District,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            TaxCode = request.TaxCode,
            BusinessLicenseUrl = uploadPromises[0],
            OperatingLicenseUrl = uploadPromises[1],
            ProfilePictureUrl = uploadPromises[2],
            BankName = request.BankName,
            BankAccountNumber = request.BankAccountNumber,
            IsParent = true,
            Status = 0,
            TotalApply = 1,
            OperatingLicenseExpiryDate = DateTimeOffset.Parse(request.OperatingLicenseExpiryDate)
        };
    }

    private async Task UpdateClinicImages(
        Clinic clinic,
        CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request)
    {
        // Only update images that were provided
        if (request.BusinessLicense != null)
        {
            clinic.BusinessLicenseUrl = await mediaService.UploadImageAsync(request.BusinessLicense);
        }

        if (request.OperatingLicense != null)
        {
            clinic.OperatingLicenseUrl = await mediaService.UploadImageAsync(request.OperatingLicense);
        }

        if (request.ProfilePictureUrl != null)
        {
            clinic.ProfilePictureUrl = await mediaService.UploadImageAsync(request.ProfilePictureUrl);
        }
    }

    private static void UpdateClinicDetails(Clinic clinic,
        CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request)
    {
        clinic.Name = request.Name;
        clinic.City = request.City;
        clinic.Ward = request.Ward;
        clinic.PhoneNumber = request.PhoneNumber;
        clinic.TaxCode = request.TaxCode;
        clinic.District = request.District;
        clinic.Address = request.Address;
        clinic.BankName = request.BankName;
        clinic.BankAccountNumber = request.BankAccountNumber;
        clinic.OperatingLicenseExpiryDate = DateTimeOffset.Parse(request.OperatingLicenseExpiryDate);
    }

    private ClinicOnBoardingRequest CreateOnboardingRequest(Guid clinicId)
    {
        return new ClinicOnBoardingRequest
        {
            Id = Guid.NewGuid(),
            ClinicId = clinicId,
            IsMain = true,
            Status = 0,
            SendMailDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, _vietnamTimeZone)
        };
    }
}