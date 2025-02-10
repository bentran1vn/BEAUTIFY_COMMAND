using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;
using Microsoft.AspNetCore.Http;


namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics;
public static class Commands
{
    public record ClinicApplyCommand(
        string Name, string Email, string PhoneNumber, string Address, string TaxCode,
        IFormFile BusinessLicense, IFormFile OperatingLicense, string OperatingLicenseExpiryDate,
        IFormFile ProfilePictureUrl
    ) : ICommand;
    
    public class UpdateClinicCommand : ICommand
    {
        public string ClinicId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public bool? IsActivated { get; set; }
    }
    
    public record ResponseClinicApplyCommand(
        string RequestId , string? RejectReason, int Action
        // 0 Approve, 1 Reject, 2 Banned
    ) : ICommand;
}