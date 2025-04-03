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
        var userExisted = await userRepository.FindSingleAsync(x => x.Email == request.Email, cancellationToken);
        if (userExisted != null)
            return Result.Failure(new Error("400", "Email already exists - Change to another email"));
        var clinic = await clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken);
        if (clinic == null)
            return Result.Failure(new Error("404", "Clinic not found"));
        if (!clinic.IsActivated)
            return Result.Failure(new Error("400", "Clinic is not activated"));
        var existingUser = await staffRepository.FindSingleAsync(x => x.Email == request.Email, cancellationToken);
        if (existingUser != null)
            return Result.Failure(new Error("400", "Email already exists"));
        var random = Random.Shared.Next(0, 100000);
        var Id = Guid.NewGuid();
        var role = request.RoleType == CONTRACT.Services.Clinics.Commands.Roles.DOCTOR
            ? Constant.Role.DOCTOR
            : Constant.Role.CLINIC_STAFF;
        var roleId = await roleRepository.FindSingleAsync(x => x.Name == role, cancellationToken);
        if (roleId == null)
            return Result.Failure(new Error("404", "Role not found"));
        var user = new Staff
        {
            Id = Id,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = passwordHasherService.HashPassword(request.FirstName + random),
            RoleId = roleId.Id,
            Status = 1,
            UserClinics = new List<UserClinic>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    ClinicId = request.ClinicId,
                    UserId = Id
                }
            }
        };
        staffRepository.Add(user);
        await mailService.SendMail(new MailContent
        {
            To = request.Email,
            Subject = "Account for Employee",
            Body = $"""
                    
                        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9;'>
                            <h1 style='color: #007bff; text-align: center;'>Account for Employee</h1>
                            <p style='font-size: 16px; color: #333;'>Your account has been created successfully.</p>
                            <div style='background-color: #fff; padding: 15px; border-radius: 5px; box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);'>
                                <p style='font-size: 16px;'><strong>Username:</strong> {request.Email}</p>
                                <p style='font-size: 16px;'><strong>Password:</strong> {request.FirstName + random}</p>
                            </div>
                            <p style='font-size: 14px; color: #555; margin-top: 20px;'>Please change your password after logging in.</p>
                        </div>
                        
                    """
        });
        return Result.Success();
    }
}