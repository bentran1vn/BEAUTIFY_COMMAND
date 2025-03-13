using BEAUTIFY_COMMAND.DOMAIN.Exceptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class ClinicUpdateAccountOfEmployeeCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicUpdateAccountOfEmployeeCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicUpdateAccountOfEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.UserId, cancellationToken) ??
                   throw new UserException.UserNotFoundException(request.UserId);

        if (user.Role?.Name == Constant.Role.CLINIC_ADMIN) throw new UnauthorizedAccessException();

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.City = request.City;
        user.Ward = request.Ward;
        user.District = request.District;
        user.Address = request.Address;

        user.PhoneNumber = request.PhoneNumber;
        if (request.ProfilePicture != null)
            user.ProfilePicture = await mediaService.UploadImageAsync(request.ProfilePicture);

        return Result.Success();
    }
}