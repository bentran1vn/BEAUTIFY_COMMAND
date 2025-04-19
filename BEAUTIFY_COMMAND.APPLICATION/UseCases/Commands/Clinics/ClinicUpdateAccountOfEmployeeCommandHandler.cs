namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class ClinicUpdateAccountOfEmployeeCommandHandler(
    IRepositoryBase<Staff, Guid> staffRepository,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.ClinicUpdateAccountOfEmployeeCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.ClinicUpdateAccountOfEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var user = await staffRepository.FindByIdAsync(request.UserId, cancellationToken) ??
                   throw new UserException.UserNotFoundException(request.UserId);

        if (user.Role?.Name == Constant.Role.CLINIC_ADMIN) throw new UnauthorizedAccessException();

        var userPhone = await staffRepository
            .FindAll(x => x.PhoneNumber != null && x.PhoneNumber.Equals(request.PhoneNumber))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(userPhone != null && userPhone.Id != request.UserId)
            return Result.Failure(new Error("400", "Phone number already exists"));

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