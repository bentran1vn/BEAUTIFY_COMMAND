using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
internal sealed class ClinicDeleteAccountOfEmployeeCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<UserClinic, Guid> userClinicRepository)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicDeleteAccountOfEmployeeCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicDeleteAccountOfEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        //need to check credential in the token in the future


        var user = await userRepository.FindByIdAsync(request.UserId, cancellationToken) ??
                   throw new UserException.UserNotFoundException(request.UserId);
        if (user?.Role?.Name == Constant.Role.CLINIC_ADMIN) throw new UnauthorizedAccessException();

        var clinic = await clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken) ??
                     throw new ClinicException.ClinicNotFoundException(request.ClinicId);
        if (user?.UserClinics.FirstOrDefault().ClinicId != clinic.Id) throw new UnauthorizedAccessException();

        userRepository.Remove(user);
        userClinicRepository.Remove(user.UserClinics?.FirstOrDefault());
        return Result.Success();
    }
}