namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Customers;
internal sealed class UpdateCustomerCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    ICurrentUserService currentUserService,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Customers.Commands.UpdateCustomerCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Customers.Commands.UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        // Get current user ID from the token
        var userId = currentUserService.UserId;

        // Find the user
        var user = await userRepository.FindByIdAsync(userId.Value, cancellationToken);
        if (user == null)
            return Result.Failure(new Error("404", "User not found"));

        // Update user properties if provided
        if (request.FirstName != null)
            user.FirstName = request.FirstName;

        if (request.LastName != null)
            user.LastName = request.LastName;

        if (request.PhoneNumber != null)
            user.PhoneNumber = request.PhoneNumber;

        if (request.City != null)
            user.City = request.City;

        if (request.District != null)
            user.District = request.District;

        if (request.Ward != null)
            user.Ward = request.Ward;

        if (request.Address != null)
            user.Address = request.Address;

        if (request.DateOfBirth != null)
            user.DateOfBirth = request.DateOfBirth;

        // Handle profile picture upload if provided
        if (request.ProfilePicture != null)
            user.ProfilePicture = await mediaService.UploadImageAsync(request.ProfilePicture);

        return Result.Success();
    }
}