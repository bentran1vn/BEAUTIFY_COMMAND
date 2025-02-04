using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics;
public static class Commands
{
    public record ClinicApplyCommand(
        string Name, string Email, string PhoneNumber, string Address, string TaxCode,
        string BusinessLicenseUrl, string OperatingLicenseUrl, string OperatingLicenseExpiryDate,
        string ProfilePictureUrl
    ) : ICommand;
}