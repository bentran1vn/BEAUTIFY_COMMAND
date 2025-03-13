using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices;
public class Commands
{
    public record CreateClinicServiceBody(
        string ClinicId,
        string Name,
        IFormFileCollection CoverImages,
        string Description,
        IFormFileCollection DescriptionImages,
        Guid CategoryId);

    public record CreateClinicServiceCommand(
        List<Guid> ClinicId,
        string Name,
        IFormFileCollection CoverImages,
        string Description,
        IFormFileCollection DescriptionImages,
        Guid CategoryId) : ICommand;

    public class UpdateClinicServiceBody
    {
        public Guid Id { get; set; }
        public string ClinicId { get; set; }
        public string Name { get; set; }

        public string? IndexCoverImagesChange { get; set; }
        public IFormFileCollection? CoverImages { get; set; }
        public string Description { get; set; }

        public string? IndexDescriptionImagesChange { get; set; }
        public IFormFileCollection? DescriptionImages { get; set; }
        public Guid CategoryId { get; set; }
    }

    public record UpdateClinicServiceCommand(
        Guid Id,
        Guid UserId,
        List<Guid> ClinicId,
        string Name,
        List<int>? IndexCoverImagesChange,
        IFormFileCollection? CoverImages,
        string Description,
        List<int>? IndexDescriptionImagesChange,
        IFormFileCollection? DescriptionImages,
        Guid CategoryId) : ICommand;

    public record DeleteClinicServiceCommand(Guid Id) : ICommand;
}