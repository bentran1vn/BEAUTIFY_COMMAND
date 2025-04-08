using BEAUTIFY_COMMAND.CONTRACT.MailTemplates;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class ClinicCreateAccountForEmployeeCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IRepositoryBase<User, Guid> userRepository,
    IPasswordHasherService passwordHasherService,
    IMailService mailService,
    IRepositoryBase<Role, Guid> roleRepository,
    IRepositoryBase<Clinic, Guid> clinicRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicCreateAccountForEmployeeCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicCreateAccountForEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        // Input validation
        if (string.IsNullOrWhiteSpace(request.Email))
            return Result.Failure(new Error("400", "Email is required"));

        // Check for existing users
        var userExisted = await userRepository.FindSingleAsync(x => x.Email == request.Email, cancellationToken);
        if (userExisted != null)
            return Result.Failure(new Error("400", "Email already exists - Please use another email"));

        var existingStaff = await staffRepository.FindSingleAsync(x => x.Email == request.Email, cancellationToken);
        if (existingStaff != null)
            return Result.Failure(new Error("400", "Email already exists as staff member"));

        // Validate clinic
        var clinic = await clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken);
        if (clinic == null)
            return Result.Failure(new Error("404", "Clinic not found"));
        if (!clinic.IsActivated)
            return Result.Failure(new Error("400", "Clinic is not activated"));

        // Get role
        var role = request.RoleType == CONTRACT.Services.Clinics.Commands.Roles.DOCTOR
            ? Constant.Role.DOCTOR
            : Constant.Role.CLINIC_STAFF;

        var roleEntity = await roleRepository.FindSingleAsync(x => x.Name == role, cancellationToken);
        if (roleEntity == null)
            return Result.Failure(new Error("404", "Role not found"));

        // Generate secure temporary password
        var temporaryPassword = GenerateTemporaryPassword(request.FirstName);
        var userId = Guid.NewGuid();

        // Create staff user
        var user = new Staff
        {
            Id = userId,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = passwordHasherService.HashPassword(temporaryPassword),
            RoleId = roleEntity.Id,
            Status = 1, // Assuming 1 means active
            UserClinics = new List<UserClinic>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    ClinicId = request.ClinicId,
                    UserId = userId
                }
            },
             // Add this field to your Staff entity if not exists
        };

        staffRepository.Add(user);

        // Send email with the new account details
        await mailService.SendMail(new MailContent
        {
            To = request.Email,
            Subject = $"Your {role.Replace("_", " ")} Account Has Been Created",
            Body = EmployeeAccountEmailTemplates.GetAccountCreationTemplate(
                request.Email, 
                request.FirstName, 
                temporaryPassword,
                role)
        });

        return Result.Success();
    }

    private static string GenerateTemporaryPassword(string firstName)
    {
        // More secure password generation
        const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        var random = new Random();
        var chars = new char[12];
        
        // Ensure at least one uppercase, one lowercase, one digit and one special char
        chars[0] = validChars[random.Next(0, 26)]; // uppercase
        chars[1] = validChars[random.Next(26, 52)]; // lowercase
        chars[2] = validChars[random.Next(52, 62)]; // digit
        chars[3] = validChars[random.Next(62, validChars.Length)]; // special
        
        for (int i = 4; i < chars.Length; i++)
        {
            chars[i] = validChars[random.Next(validChars.Length)];
        }
        
        // Shuffle the array
        for (int i = 0; i < chars.Length; i++)
        {
            var r = random.Next(i, chars.Length);
            (chars[r], chars[i]) = (chars[i], chars[r]);
        }
        
        return new string(chars);
    }
}