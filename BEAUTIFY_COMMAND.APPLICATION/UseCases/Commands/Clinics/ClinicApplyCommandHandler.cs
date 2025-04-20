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

        if (request.RoleName != null || request.ClinicId != null)
        {
            if (!(request.RoleName != null && request.ClinicId != null))
            {
                return Result.Failure(new Error("500", "Must have RoleName and ClinicId"));
            }
        }
        
        if (request.RoleName != null && request.ClinicId != null)
        {
            if(request.RoleName != "Clinic Admin")
            {
                return Result.Failure(new Error("403", "Not Authorized"));
            }
            
            var clinic = await clinicRepository
                .FindByIdAsync((Guid)request.ClinicId, cancellationToken);

            if (clinic == null)
            {
                throw new Exception($"Clinic {request.ClinicId} not found"); 
            }
            
            if(clinic.Email != request.Email)
            {
                return Result.Failure(new Error("400", "Clinic Email not match"));
            }
            
            if(clinic.PhoneNumber != request.PhoneNumber)
            {
                return Result.Failure(new Error("400", "Clinic Phone Number not match"));
            }
            
            if(clinic.TaxCode != request.TaxCode)
            {
                return Result.Failure(new Error("400", "Clinic Tax Code not match"));
            }
            
            if(request.BankName == null || request.BankAccountNumber == null)
            {
                return Result.Failure(new Error("500", "Must have BankName and BankAccountNumber"));
            }

            if (request.BusinessLicense != null)
            {
                var businessLicenseUrl = await mediaService.UploadImageAsync(request.BusinessLicense);
                clinic.BusinessLicenseUrl = businessLicenseUrl;
            }
            
            if (request.OperatingLicense != null)
            {
                var operatingLicenseUrl = await mediaService.UploadImageAsync(request.OperatingLicense);
                clinic.OperatingLicenseUrl = operatingLicenseUrl;
            }
            
            if (request.ProfilePictureUrl != null)
            {
                var profilePictureUrl = await mediaService.UploadImageAsync(request.ProfilePictureUrl);
                clinic.ProfilePictureUrl = profilePictureUrl;
            }
            
            clinic.Name = request.Name;
            clinic.City = request.City;
            clinic.Ward = request.Ward;
            clinic.District = request.District;
            clinic.Address = request.Address;
            clinic.BankName = request.BankName;
            clinic.BankAccountNumber = request.BankAccountNumber;
            clinic.Status = 0;
            clinic.TotalApply += 1;
            clinic.OperatingLicenseExpiryDate = DateTimeOffset.Parse(request.OperatingLicenseExpiryDate);
            
            var clinicOnBoardingRequest = new ClinicOnBoardingRequest
            {
                Id = Guid.NewGuid(),
                ClinicId = clinic.Id,
                IsMain = true,
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
            
            return Result.Success("Clinic Apply Successfully");
        }
        
        if(request.BusinessLicense == null || request.OperatingLicense == null || request.ProfilePictureUrl == null || request.OperatingLicenseExpiryDate == null)
        {
            return Result.Failure(new Error("500", "Must have BusinessLicense, OperatingLicense and ProfilePictureUrl"));
        }
        
        // Get all clinics that match any of the fields in a single query
        var existingClinics = await clinicRepository
            .FindAll(x =>
                (x.Email == request.Email || x.TaxCode == request.TaxCode || x.PhoneNumber == request.PhoneNumber) &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);

        // Check if any field already exists
        if (existingClinics.Count != 0)
        {
            var duplicateFields = new List<string>();
            if (existingClinics.Any(x => x.Email == request.Email)) duplicateFields.Add("Email");
            if (existingClinics.Any(x => x.TaxCode == request.TaxCode)) duplicateFields.Add("Tax Code");
            if (existingClinics.Any(x => x.PhoneNumber == request.PhoneNumber)) duplicateFields.Add("Phone Number");

            return Result.Failure(new Error("400",
                $"The following information already exists: {string.Join(", ", duplicateFields)}"));
        }

        // If we need the clinic with all its related data for further processing
        // Since we've already checked that no clinic exists with any of the fields,
        // this will return null, but we'll keep it for code clarity
        var isExist = await clinicRepository
            .FindAll(
                x => x.Email == request.Email && x.TaxCode == request.TaxCode && x.PhoneNumber == request.PhoneNumber &&
                     !x.IsDeleted,
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
                IsMain = true,
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
                IsMain = true,
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