namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Users;
internal sealed class UpdateUserProfileCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<Staff, Guid> staffRepository,
    ICurrentUserService currentUserService,
    IMediaService mediaService)
    : ICommandHandler<CONTRACT.Services.Users.Commands.UpdateUserProfileCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Users.Commands.UpdateUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        // Get current user ID from the token
        var userId = currentUserService.UserId;
        // Get user role from the token
        var userRole = currentUserService.Role;
        return userRole switch
        {
            // Handle based on user role
            Constant.Role.CUSTOMER => await UpdateCustomerProfile(userId.Value, request, cancellationToken),
            Constant.Role.DOCTOR => await UpdateDoctorProfile(userId.Value, request, cancellationToken),
            _ => Result.Failure(new Error("403",
                "Only customers and doctors can update their profile using this endpoint"))
        };
    }

    private async Task<Result> UpdateCustomerProfile(Guid userId, CONTRACT.Services.Users.Commands.UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        // Find the user
        var user = await userRepository.FindByIdAsync(userId, cancellationToken);
        if (user == null)
            throw new UserException.UserNotFoundException(userId);

        // Update user properties
        await UpdateUserProperties(user, request, cancellationToken);

        return Result.Success("Customer profile updated successfully");
    }

    private async Task<Result> UpdateDoctorProfile(Guid userId, CONTRACT.Services.Users.Commands.UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        // Find the doctor
        var doctor = await staffRepository.FindByIdAsync(userId, cancellationToken);
        if (doctor == null)
            throw new UserException.UserNotFoundException(userId);

        // Verify that the user is a doctor
        if (doctor.Role?.Name != Constant.Role.DOCTOR)
            return Result.Failure(new Error("403", "Only doctors can update their profile using this endpoint"));

        // Update doctor properties
        await UpdateStaffProperties(doctor, request, cancellationToken);

        return Result.Success("Doctor profile updated successfully");
    }

    private async Task UpdateUserProperties(User user, CONTRACT.Services.Users.Commands.UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
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
    }

    private async Task UpdateStaffProperties(Staff staff, CONTRACT.Services.Users.Commands.UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        // Update staff properties if provided
        if (request.FirstName != null)
            staff.FirstName = request.FirstName;

        if (request.LastName != null)
            staff.LastName = request.LastName;

        if (request.PhoneNumber != null)
            staff.PhoneNumber = request.PhoneNumber;

        if (request.City != null)
            staff.City = request.City;

        if (request.District != null)
            staff.District = request.District;

        if (request.Ward != null)
            staff.Ward = request.Ward;

        if (request.Address != null)
            staff.Address = request.Address;

        if (request.DateOfBirth != null)
            staff.DateOfBirth = request.DateOfBirth;

        // Handle profile picture upload if provided
        if (request.ProfilePicture != null)
            staff.ProfilePicture = await mediaService.UploadImageAsync(request.ProfilePicture);
    }
}
