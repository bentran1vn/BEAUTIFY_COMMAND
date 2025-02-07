using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;

public class ClinicApplyCommandHandler : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicApplyCommand>
{
    private readonly IRepositoryBase<DOMAIN.Entities.Clinics, Guid> _clinicRepository;
    // private readonly IRepositoryBase<User, Guid> _userRepository;
    private readonly IRepositoryBase<ClinicOnBoardingRequest, Guid> _clinicOnBoardingRequestRepository;
    private readonly IMailService _mailService;
    private readonly IMediaService _mediaService;

    public ClinicApplyCommandHandler(IRepositoryBase<DOMAIN.Entities.Clinics, Guid> clinicRepository, IRepositoryBase<ClinicOnBoardingRequest, Guid> clinicOnBoardingRequestRepository, IMailService mailService, IMediaService mediaService)
    {
        _clinicRepository = clinicRepository;
        _clinicOnBoardingRequestRepository = clinicOnBoardingRequestRepository;
        _mailService = mailService;
        _mediaService = mediaService;
        // _userRepository = userRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicApplyCommand request, CancellationToken cancellationToken)
    {
        TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        
        var isExist = await _clinicRepository
            .FindSingleAsync(
                x => x.Email == request.Email || x.TaxCode == request.TaxCode
                    || x.PhoneNumber == request.PhoneNumber, cancellationToken,
                x => x.ClinicOnBoardingRequests!);

        if (isExist != null)
        {
            if (isExist.TaxCode == request.TaxCode && isExist.PhoneNumber == request.PhoneNumber &&
                isExist.Email == request.Email)
            {
                // Re-Apply
                if (isExist.Status == 1)
                {
                    return Result.Failure(new Error("500", "Clinics already apply successfully !"));
                }
                
                if (isExist.Status == 0)
                {
                    return Result.Failure(new Error("500", "Clinics Request is handling !"));
                }
                
                if (isExist.Status == 3)
                {
                    return Result.Failure(new Error("500", "Clinics is banned !"));
                }

                if (isExist.ClinicOnBoardingRequests == null || !isExist.ClinicOnBoardingRequests!.Any())
                {
                    return Result.Failure(new Error("500", "ClinicOnBoardingRequests Not Exist"));
                }
                
                var newestApply = isExist.ClinicOnBoardingRequests!.OrderByDescending(x => x.CreatedOnUtc).First();
                
                var now = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);

                if (now < newestApply.SendMailDate.AddDays(30))
                {
                    return Result.Failure(new Error("500", $"Your request has been rejected ! Please comeback {newestApply.SendMailDate.AddDays(30) - now} days"));
                }
                
                isExist.TotalApply += 1;
                
                // Check Send Date
                var clinicOnBoardingRequest = new ClinicOnBoardingRequest()
                {
                    Id = Guid.NewGuid(),
                    ClinicId = isExist.Id,
                    Status = 0,
                    SendMailDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone),
                };
                
                _clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);
            }
            else
            {
                // Already Information Taken
                return Result.Failure(new Error("500", "Information Already Exist"));
            }
        }
        else
        {
            var uploadPromises = await Task.WhenAll(
                _mediaService.UploadImageAsync(request.BusinessLicense),
                _mediaService.UploadImageAsync(request.OperatingLicense),
                _mediaService.UploadImageAsync(request.ProfilePictureUrl)
            );
            var businessLicenseUrl = uploadPromises[0];
            var operatingLicenseUrl = uploadPromises[1];
            var profilePictureUrl = uploadPromises[2];
            
            var clinic = new DOMAIN.Entities.Clinics()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                TaxCode = request.TaxCode,
                BusinessLicenseUrl = businessLicenseUrl,
                OperatingLicenseUrl = operatingLicenseUrl,
                IsParent = true,
                Status = 0,
                ProfilePictureUrl = profilePictureUrl,
                TotalApply = 1,
                OperatingLicenseExpiryDate = DateTimeOffset.Parse(request.OperatingLicenseExpiryDate),
            };
        
            _clinicRepository.Add(clinic);
            
            var clinicOnBoardingRequest = new ClinicOnBoardingRequest()
            {
                Id = Guid.NewGuid(),
                ClinicId = clinic.Id,
                Status = 0,
                SendMailDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone),
            };
        
            _clinicOnBoardingRequestRepository.Add(clinicOnBoardingRequest);
            
            await _mailService.SendMail(new MailContent
            {
                To = request.Email,
                Subject = $"Your Request Has Been Registered !",
                Body = $@"
                    <p>Dear {request.Email},</p>
                    <p> System regis your information: </p>
                    <p> Clinic contact email: {request.Email}</p>
                    <p> Clinic contact phone number: {request.PhoneNumber}</p>
                    <p> Clinic contact tax code: {request.TaxCode}</p>
                    <p>Thank you for your application !</p>
                    <p>Our system will handle as soon as possible !</p>
                ",
            });
        }
        
        return Result.Success("Clinic Apply Successfully");
    }
}