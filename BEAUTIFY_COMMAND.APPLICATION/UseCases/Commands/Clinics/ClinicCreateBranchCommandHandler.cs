namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class
    ClinicCreateBranchCommandHandler(
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IRepositoryBase<ClinicOnBoardingRequest, Guid> clinicOnBoardingRequestRepository,
        IMediaService mediaService,
        IRepositoryBase<Role, Guid> roleRepository,
        ICurrentUserService currentUserService,
        IMailService mailService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicCreateBranchCommand request,
        CancellationToken cancellationToken)
    {
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        var parentClinic =
            await clinicRepository.FindByIdAsync(currentUserService.ClinicId!.Value, cancellationToken) ??
            throw new ClinicException.ClinicNotFoundException(currentUserService.ClinicId.Value);

        if (parentClinic.TotalBranches >= parentClinic.AdditionBranches)
            return Result.Failure(new Error("403", "You have reached the maximum number of branches"));

        var isExist = await clinicRepository
            .FindAll(x => x.PhoneNumber == request.PhoneNumber && x.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);

        var isExistEmail = await clinicRepository
            .FindAll(x => x.PhoneNumber == request.Email && x.IsDeleted == false
                                                         && x.IsActivated && x.Status == 1)
            .FirstOrDefaultAsync(cancellationToken);

        var role = await roleRepository.FindSingleAsync(x => x.Name == "Clinic Staff", cancellationToken);

        if (role == null)
            return Result.Failure(new Error("404", "Role not found"));

        if (isExist is { IsActivated: true, Status: 1 })
            return Result.Failure(new Error("500", "Clinic with phone number already exists"));

        if (isExistEmail is { IsActivated: true, Status: 1 } && isExistEmail.Id != currentUserService.ClinicId.Value)
        {
            return Result.Failure(new Error("500", "Clinic with email already exists"));
        }

        if (isExist is { IsActivated: false, Status: 0 })
        {
            isExist.Email = request.Email;
            isExist.Name = request.Name;
            isExist.City = request.City;
            isExist.Ward = request.Ward;
            isExist.District = request.District;
            isExist.Address = request.Address;
            isExist.WorkingTimeStart = request.WorkingTimeStart;
            isExist.WorkingTimeEnd = request.WorkingTimeEnd;
            isExist.TaxCode = parentClinic.TaxCode;

            if (request.OperatingLicense != null)
            {
                var oUrl = await mediaService.UploadImageAsync(request.OperatingLicense);
                isExist.OperatingLicenseUrl = oUrl;
            }

            if (request.ProfilePictureUrl != null)
            {
                var pUrl = await mediaService.UploadImageAsync(request.ProfilePictureUrl);
                isExist.ProfilePictureUrl = pUrl;
            }

            isExist.BankName = request.BankName;
            isExist.BankAccountNumber = request.BankAccountNumber;
            isExist.OperatingLicenseExpiryDate = request.OperatingLicenseExpiryDate;

            clinicRepository.Update(isExist);

            var clinicOnBoardingRequest = new ClinicOnBoardingRequest
            {
                Id = Guid.NewGuid(),
                ClinicId = isExist.Id,
                IsMain = false,
                Status = 0,
                SendMailDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone)
            };

            clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);
        }
        else
        {
            if (request.OperatingLicense == null)
                return Result.Failure(new Error("400", "Missing Operating License"));

            var oUrl = await mediaService.UploadImageAsync(request.OperatingLicense);

            var clinic = new Clinic
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Name = request.Name,
                City = request.City,
                Ward = request.Ward,
                District = request.District,
                Address = request.Address,
                ParentId = currentUserService.ClinicId.Value,
                WorkingTimeStart = parentClinic.WorkingTimeStart,
                WorkingTimeEnd = parentClinic.WorkingTimeEnd,
                TaxCode = parentClinic.TaxCode,
                BusinessLicenseUrl = parentClinic.BusinessLicenseUrl,
                OperatingLicenseUrl = oUrl,
                OperatingLicenseExpiryDate = request.OperatingLicenseExpiryDate,
                Status = 0,
                BankName = request.BankName,
                BankAccountNumber = request.BankAccountNumber,
                IsActivated = false
            };

            if (request.ProfilePictureUrl != null)
            {
                var pUrl = await mediaService.UploadImageAsync(request.ProfilePictureUrl);
                clinic.ProfilePictureUrl = pUrl;
            }

            clinicRepository.Add(clinic);


            var clinicOnBoardingRequest = new ClinicOnBoardingRequest
            {
                Id = Guid.NewGuid(),
                ClinicId = clinic.Id,
                IsMain = false,
                Status = 0,
                SendMailDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone)
            };

            clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);
        }

        await mailService.SendMail(new MailContent
        {
            To = request.Email,
            Subject = "Your Request Has Been Registered !",
            Body = $@"
                    <p>Dear {request.Email},</p>
                    <p> System regis your information: </p>
                    <p> Clinic contact email: {request.Email}</p>
                    <p> Clinic contact phone number: {request.PhoneNumber}</p>
                    <p>Thank you for your clinic branch create application !</p>
                    <p>Our system will handle as soon as possible !</p>
                "
        });

        return Result.Success("Send Branch Create Request Successfully");
    }
}