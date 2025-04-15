using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Customers;
public static class Commands
{
    public record UpdateCustomerCommand(
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
