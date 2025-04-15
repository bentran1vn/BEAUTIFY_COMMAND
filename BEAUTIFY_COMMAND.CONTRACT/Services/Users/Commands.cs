using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Users;
public static class Commands
{
    public record UpdateUserProfileCommand(
        string? FirstName,
        string? LastName,
        string? PhoneNumber,
        string? City,
        string? District,
        string? Ward,
        string? Address,
        DateOnly? DateOfBirth,
        IFormFile? ProfilePicture) : ICommand;
}
