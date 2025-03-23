using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.DoctorCertificate;
public static class Commands
{
    public record CreateDoctorCertificateCommand(
        string CertificateName,
        IFormFile CertificateFile,
        DateTimeOffset ExpiryDate,
        string? Note) : ICommand;

    public record DeleteCommand(Guid Id) : ICommand;

    public class UpdateCommand : ICommand
    {
        public Guid Id { get; set; }

        public string CertificateName { get; set; } = string.Empty;
        public IFormFile? CertificateFile { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public string? Note { get; set; }
    }
}