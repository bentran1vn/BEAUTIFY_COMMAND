namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class ClinicApplyCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<ClinicOnBoardingRequest, Guid> clinicOnBoardingRequestRepository,
    IMailService mailService,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicApplyCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request,
        CancellationToken cancellationToken)
    {
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        // Get all clinics that match any of the fields in a single query
        var existingClinics = await clinicRepository
            .FindAll(x => (x.Email == request.Email || x.TaxCode == request.TaxCode || x.PhoneNumber == request.PhoneNumber) && !x.IsDeleted)
            .ToListAsync(cancellationToken);

        // Check if any field already exists
        if (existingClinics.Count != 0)
        {
            var duplicateFields = new List<string>();
            if (existingClinics.Any(x => x.Email == request.Email)) duplicateFields.Add("Email");
            if (existingClinics.Any(x => x.TaxCode == request.TaxCode)) duplicateFields.Add("Tax Code");
            if (existingClinics.Any(x => x.PhoneNumber == request.PhoneNumber)) duplicateFields.Add("Phone Number");

            return Result.Failure(new Error("400", $"The following information already exists: {string.Join(", ", duplicateFields)}"));
        }

        // If we need the clinic with all its related data for further processing
        // Since we've already checked that no clinic exists with any of the fields,
        // this will return null, but we'll keep it for code clarity
        var isExist = await clinicRepository
            .FindAll(x => (x.Email == request.Email && x.TaxCode == request.TaxCode && x.PhoneNumber == request.PhoneNumber) && !x.IsDeleted,
                x => x.ClinicOnBoardingRequests!)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken);

        // Since we've already checked for duplicates above, if isExist is not null here,
        // it means all three fields match (email, tax code, and phone number)
        if (isExist != null)
        {
            switch (isExist.Status)
            {
                // Re-Apply
                case 1:
                    return Result.Failure(new Error("400", "Clinics already apply successfully !"));
                case 0:
                    return Result.Failure(new Error("400", "Clinics Request is handling !"));
                case 3:
                    return Result.Failure(new Error("400", "Clinics is banned !"));
            }

            if (isExist.ClinicOnBoardingRequests == null || isExist.ClinicOnBoardingRequests!.Count == 0)
                return Result.Failure(new Error("404", "ClinicOnBoardingRequests Not Exist"));

            isExist.TotalApply += 1;

            // Check Send Date
            var clinicOnBoardingRequest = new ClinicOnBoardingRequest
            {
                Id = Guid.NewGuid(),
                ClinicId = isExist.Id,
                Status = 0,
                SendMailDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone)
            };

            clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);
        }
        else
        {
            var uploadPromises = await Task.WhenAll(
                mediaService.UploadImageAsync(request.BusinessLicense),
                mediaService.UploadImageAsync(request.OperatingLicense),
                mediaService.UploadImageAsync(request.ProfilePictureUrl)
            );
            var businessLicenseUrl = uploadPromises[0];
            var operatingLicenseUrl = uploadPromises[1];
            var profilePictureUrl = uploadPromises[2];

            var clinic = new Clinic
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                City = request.City,
                Ward = request.Ward,
                District = request.District,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                BankName = request.BankName,
                BankAccountNumber = request.BankAccountNumber,
                TaxCode = request.TaxCode,
                BusinessLicenseUrl = businessLicenseUrl,
                OperatingLicenseUrl = operatingLicenseUrl,
                IsParent = true,
                Status = 0,
                ProfilePictureUrl = profilePictureUrl,
                TotalApply = 1,
                OperatingLicenseExpiryDate = DateTimeOffset.Parse(request.OperatingLicenseExpiryDate)
            };

            clinicRepository.Add(clinic);

            var clinicOnBoardingRequest = new ClinicOnBoardingRequest
            {
                Id = Guid.NewGuid(),
                ClinicId = clinic.Id,
                Status = 0,
                SendMailDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone)
            };

            clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);

            await mailService.SendMail(new MailContent
            {
                To = request.Email,
                Subject = "Your Request Has Been Registered !",
                Body = $@"
                    <p>Dear {request.Email},</p>
                    <p> System regis your information: </p>
                    <p> Clinic contact email: {request.Email}</p>
                    <p> Clinic contact phone number: {request.PhoneNumber}</p>
                    <p> Clinic contact tax code: {request.TaxCode}</p>
                    <p>Thank you for your application !</p>
                    <p>Our system will handle as soon as possible !</p>
                "
            });
        }

        return Result.Success("Clinic Apply Successfully");
    }
}