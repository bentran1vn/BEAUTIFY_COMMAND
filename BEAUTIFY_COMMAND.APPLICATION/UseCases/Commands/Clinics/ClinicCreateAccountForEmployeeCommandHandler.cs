using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class ClinicCreateAccountForEmployeeCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    ICurrentUserService currentUserService,
    IPasswordHasherService passwordHasherService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicCreateAccountForEmployeeCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicCreateAccountForEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        // var clinicId = currentUserService.ClinicId ?? throw new UnauthorizedAccessException();
        var Id = Guid.NewGuid();
        var user = new User
        {
            Id = Id,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = passwordHasherService.HashPassword(request.Password),
            RoleId = request.RoleId,
            Status = 1,
            UserClinics = new List<UserClinic>
            {
                new()
                {
                    ClinicId = request.ClinicId,
                    UserId = Id
                }
            }
        };
        userRepository.Add(user);
        return Result.Success();
    }
}