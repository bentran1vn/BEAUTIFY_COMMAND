using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Users;
public static class Commands
{
    public class UpdateUserProfileCommand : ICommand
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}