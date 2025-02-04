using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;

public class ResponseClinicApplyCommandHandler: ICommandHandler<CONTRACT.Services.Clinics.Commands.ResponseClinicApplyCommand>
{
    // private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;
    private readonly IRepositoryBase<User, Guid> _userRepository;
    private readonly IRepositoryBase<ClinicOnBoardingRequest, Guid> _clinicOnBoardingRequestRepository;
    private readonly IMailService _mailService;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IRepositoryBase<UserClinic, Guid> _userClinicRepository;

    public ResponseClinicApplyCommandHandler(IRepositoryBase<User, Guid> userRepository, IRepositoryBase<ClinicOnBoardingRequest, Guid> clinicOnBoardingRequestRepository, IMailService mailService, IPasswordHasherService passwordHasherService, IRepositoryBase<UserClinic, Guid> userClinicRepository)
    {
        // _clinicRepository = clinicRepository;
        _userRepository = userRepository;
        _clinicOnBoardingRequestRepository = clinicOnBoardingRequestRepository;
        _mailService = mailService;
        _passwordHasherService = passwordHasherService;
        _userClinicRepository = userClinicRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ResponseClinicApplyCommand request, CancellationToken cancellationToken)
    {
        var applyRequest = await _clinicOnBoardingRequestRepository.FindByIdAsync(new Guid(request.RequestId), cancellationToken, x => x.Clinic!);

        if (applyRequest == null)
        {
            return Result.Failure(new Error("500", "Clinic Request Not Found"));
        }

        if (applyRequest!.Status != 0 || applyRequest.IsDeleted)
        {
            return Result.Failure(new Error("500", "Clinic Apply Request is Handled"));
        }

        if (request.Action != 0 && request.RejectReason == null)
        {
            return Result.Failure(new Error("500", "Missing Reject reason for Rejected or Banned Response"));
        }
        
        MailContent content = new MailContent();
        content.To = applyRequest.Clinic!.Email;
        content.Subject = "Response the Clinic Apply Request";
        applyRequest.Status = request.Action + 1;
        
        if (request.Action != 0)
        {
            applyRequest.RejectReason = request.RejectReason;
            applyRequest.Clinic!.Status = request.Action + 1;
            
            if (request.Action % 2 == 0) // 2 is Banned
            {
                content.Body = $@"
                    <p>Dear {applyRequest.Clinic.Email},</p>
                    <p>First of all, our System thanks you for your application!
                    But according to your application, we have to say that we are sorry that
                    you violated the standard rules of the system, so your registration request was banned ({applyRequest.RejectReason}).
                    If you have any questions, please reply to this email.</p>
                    <p>Thank you for your application !</p>
                ";
            }
            else
            {
                content.Body = $@"
                    <p>Dear {applyRequest.Clinic.Email},</p>
                    <p>First of all, our System thanks you for submitting your application!
                    But according to your application, we have to say that we regret that
                    your application still does not meet the requirements ({applyRequest.RejectReason}).,
                    so your registration request has been rejected.
                    You have 30 days to prepare again for the next application and I hope you understand this.
                    If you have any questions, please reply to this email.</p>
                    <p>Thank you for your application !</p>
                ";
            }
        }
        else
        {
            string guid = Guid.NewGuid().ToString("N").ToLower(); // Convert to lowercase
            string specialChars = "!@#$%^&*";
    
            Random random = new Random();
    
            // Ensure first character is uppercase
            var firstChar = char.ToUpper(guid[0]); 
            var specialChar = specialChars[random.Next(specialChars.Length)];
    
            // Construct the password
            var passwordRandom = $"{firstChar}{guid.Substring(1, 5)}{specialChar}{guid.Substring(6, 2)}";
            
            var hashingPassword = _passwordHasherService.HashPassword($"{passwordRandom}");
            
            var user = new User()
            {
                Email = applyRequest.Clinic!.Email,
                FirstName = "FirstNameAdmin",
                LastName = "LastNameAdmin",
                PhoneNumber = applyRequest.Clinic!.PhoneNumber,
                DateOfBirth = DateOnly.Parse("1999-01-01"),
                Address = applyRequest.Clinic!.Address,
                Password = hashingPassword,
                Status = 1,
            };
            
            _userRepository.Add(user);

            var userClinic = new UserClinic()
            {
                UserId = user.Id,
                ClinicId = applyRequest.Clinic.Id,
            };
            
            _userClinicRepository.Add(userClinic);
            
            content.Body = $@"
                <p>Dear {applyRequest.Clinic.Email},</p>
                <p>First of all, our System thanks you for submitting your application!
                We are congratulations to say that your application has been approve</p>
                <p>Your admin account for login to manage clinic: </p>
                <p>Email: {applyRequest.Clinic.Email}</p>
                <p>Password: {passwordRandom}</p>
                <p>If you have any questions, please reply to this email.</p>
                <p>Thank you for your application !</p>
            ";
        }
        
        await _mailService.SendMail(content);

        return Result.Success("Response the request successfully !");
    }
}