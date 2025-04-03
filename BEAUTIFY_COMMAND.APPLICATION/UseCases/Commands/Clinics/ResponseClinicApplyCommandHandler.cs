namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class ResponseClinicApplyCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<ClinicOnBoardingRequest, Guid> clinicOnBoardingRequestRepository,
    IMailService mailService,
    IPasswordHasherService passwordHasherService,
    IRepositoryBase<UserClinic, Guid> userClinicRepository,
    IRepositoryBase<SubscriptionPackage, Guid> subscriptionPackageRepository,
    IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ResponseClinicApplyCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ResponseClinicApplyCommand request,
        CancellationToken cancellationToken)
    {
        var applyRequest =
            await clinicOnBoardingRequestRepository.FindByIdAsync(new Guid(request.RequestId), cancellationToken,
                x => x.Clinic!);

        if (applyRequest == null) return Result.Failure(new Error("404", "Clinic Request Not Found"));

        if (applyRequest!.Status != 0 || applyRequest.IsDeleted)
            return Result.Failure(new Error("400", "Clinic Apply Request is Handled"));

        if (request.Action != 0 && request.RejectReason == null)
            return Result.Failure(new Error("400", "Missing Reject reason for Rejected or Banned Response"));

        var content = new MailContent
        {
            To = applyRequest.Clinic!.Email,
            Subject = "Response the Clinic Apply Request"
        };
        applyRequest.Status = request.Action + 1;

        if (request.Action != 0)
        {
            applyRequest.RejectReason = request.RejectReason;
            applyRequest.Clinic!.Status = request.Action + 1;

            content.Body = request.Action % 2 == 0
                ? $@"
                    <p>Dear {applyRequest.Clinic.Email},</p>
                    <p>First of all, our System thanks you for your application!
                    But according to your application, we have to say that we are sorry that
                    you violated the standard rules of the system, so your registration request was banned ({applyRequest.RejectReason}).
                    If you have any questions, please reply to this email.</p>
                    <p>Thank you for your application !</p>
                "
                : $@"
                    <p>Dear {applyRequest.Clinic.Email},</p>
                    <p>First of all, our System thanks you for submitting your application!
                    But according to your application, we have to say that we regret that
                    your application still does not meet the requirements ({applyRequest.RejectReason}).,
                    so your registration request has been rejected.
                    You have to prepare again for the next application and I hope you understand this.
                    If you have any questions, please reply to this email.</p>
                    <p>Thank you for your application !</p>
                "; // 2 is Banned
        }
        else
        {
            applyRequest.Clinic!.Status = 1;
            applyRequest.Clinic!.IsActivated = true;

            var guid = Guid.NewGuid().ToString("N").ToLower(); // Convert to lowercase
            const string specialChars = "!@#$%^&*";

            var random = new Random();

            // Ensure first character is uppercase
            var firstChar = char.ToUpper(guid[0]);
            var specialChar = specialChars[random.Next(specialChars.Length)];

            // Construct the password
            var passwordRandom = $"{firstChar}{guid.Substring(1, 5)}{specialChar}{guid.Substring(6, 2)}";

            var hashingPassword = passwordHasherService.HashPassword($"{passwordRandom}");

            var user = new Staff
            {
                Email = applyRequest.Clinic!.Email,
                FirstName = "FirstNameAdmin",
                LastName = "LastNameAdmin",
                PhoneNumber = applyRequest.Clinic!.PhoneNumber,
                DateOfBirth = DateOnly.Parse("1999-01-01"),
                //Address = applyRequest.Clinic!.Address,
                City = applyRequest.Clinic.City,
                District = applyRequest.Clinic.District,
                Ward = applyRequest.Clinic.Ward,
                Address = applyRequest.Clinic.Address,
                Password = hashingPassword,
                RoleId = new Guid("C6D93B8C-F509-4498-ABBB-FE63EDC66F2B"),
                Status = 1
            };

            staffRepository.Add(user);

            var userClinic = new UserClinic
            {
                UserId = user.Id,
                ClinicId = applyRequest.Clinic.Id
            };

            userClinicRepository.Add(userClinic);

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

            var sub = await subscriptionPackageRepository.FindSingleAsync(x => x.Name.Equals("Dùng Thử"),
                cancellationToken);

            if (sub == null) return Result.Failure(new Error("404", "Subscription package Not Found"));

            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            var trans = new SystemTransaction
            {
                Id = Guid.NewGuid(),
                ClinicId = applyRequest.Clinic.Id,
                SubscriptionPackageId = sub.Id,
                Amount = sub.Price,
                TransactionDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone),
                Status = 1
            };

            systemTransactionRepository.Add(trans);
        }

        await mailService.SendMail(content);

        return Result.Success("Response the request successfully !");
    }
}