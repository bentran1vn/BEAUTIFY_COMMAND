using BEAUTIFY_COMMAND.DOMAIN.MailTemplates;

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
            Subject = "Clinic Application Decision"
        };

        applyRequest.Status = request.Action + 1;

        if (request.Action != 0)
        {
            // 1 is Rejected
            applyRequest.RejectReason = request.RejectReason;
            applyRequest.Clinic!.Status = request.Action + 1;

            if (request.Action == 1)
            {
                var userExist = staffRepository
                    .FindAll(x => x.Email.Equals(applyRequest.Clinic!.Email) &&
                                  x.IsDeleted == false)
                    .FirstOrDefault();

                if (userExist != null)
                {
                    content.Body = ClinicApplicationEmailTemplates.GetRejected1Template(applyRequest.Clinic.Email,
                        applyRequest.RejectReason ?? "");

                    userExist.FirstName = applyRequest.Clinic.Name;
                    userExist.LastName = "";
                    userExist.PhoneNumber = applyRequest.Clinic!.PhoneNumber;
                    userExist.DateOfBirth = DateOnly.Parse("1999-01-01");
                    userExist.City = applyRequest.Clinic.City;
                    userExist.District = applyRequest.Clinic.District;
                    userExist.Ward = applyRequest.Clinic.Ward;
                    userExist.Address = applyRequest.Clinic.Address;

                    staffRepository.Update(userExist);
                }
                else
                {
                    var passwordRandom = GenerateRandomPassword();
                    var hashingPassword = passwordHasherService.HashPassword(passwordRandom);
                    var user = new Staff
                    {
                        Email = applyRequest.Clinic!.Email,
                        FirstName = applyRequest.Clinic.Name,
                        LastName = "",
                        PhoneNumber = applyRequest.Clinic!.PhoneNumber,
                        DateOfBirth = DateOnly.Parse("1999-01-01"),
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

                    content.Body = ClinicApplicationEmailTemplates.GetRejectedTemplate(applyRequest.Clinic.Email,
                        applyRequest.RejectReason, passwordRandom);
                }
            }
            else
            {
                content.Body = ClinicApplicationEmailTemplates.GetBannedTemplate(applyRequest.Clinic.Email,
                    applyRequest.RejectReason);
            }
        }
        else
        {
            // 0 is Approved
            applyRequest.Clinic!.Status = 1;
            applyRequest.Clinic!.IsActivated = true;
            applyRequest.Clinic!.IsFirstLogin = true;

            var userExist = staffRepository
                .FindAll(x => x.Email.Equals(applyRequest.Clinic!.Email) &&
                              x.IsDeleted == false)
                .FirstOrDefault();

            if (userExist != null)
            {
                userExist.FirstName = applyRequest.Clinic.Name;
                userExist.LastName = "";
                userExist.PhoneNumber = applyRequest.Clinic!.PhoneNumber;
                userExist.DateOfBirth = DateOnly.Parse("1999-01-01");
                userExist.City = applyRequest.Clinic.City;
                userExist.District = applyRequest.Clinic.District;
                userExist.Ward = applyRequest.Clinic.Ward;
                userExist.Address = applyRequest.Clinic.Address;

                staffRepository.Update(userExist);

                content.Body = ClinicApplicationEmailTemplates.GetApprovedTemplate(applyRequest.Clinic.Email);
            }
            else
            {
                var passwordRandom = GenerateRandomPassword();
                var hashingPassword = passwordHasherService.HashPassword(passwordRandom);

                var user = new Staff
                {
                    Email = applyRequest.Clinic!.Email,
                    FirstName = applyRequest.Clinic.Name,
                    LastName = "",
                    PhoneNumber = applyRequest.Clinic!.PhoneNumber,
                    DateOfBirth = DateOnly.Parse("1999-01-01"),
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

                content.Body =
                    ClinicApplicationEmailTemplates.GetApprovedTemplate(applyRequest.Clinic.Email, passwordRandom);
            }
        }

        var sub = await subscriptionPackageRepository.FindSingleAsync(x => x.Name.Equals("Dùng Thử"),
            cancellationToken);

        if (sub == null) return Result.Failure(new Error("404", "Subscription package Not Found"));

        applyRequest.Clinic.AdditionBranches = sub.LimitBranch;
        applyRequest.Clinic.AdditionLivestreams = sub.LimitLiveStream;

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

        await mailService.SendMail(content);

        return Result.Success("Response the request successfully !");
    }

    private static string GenerateRandomPassword()
    {
        var random = new Random();
        // Construct the password
        return random.Next(100000, 9999999).ToString();
    }
}