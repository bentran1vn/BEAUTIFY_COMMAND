using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices;
public class Commands
{
    public record CreateClinicServiceBody(
        string ClinicId,
        string Name,
        IFormFileCollection CoverImages,
        string Description,
        Guid CategoryId);

    public record CreateClinicServiceCommand(
        Guid ParentId,
        List<Guid> ClinicId,
        string Name,
        IFormFileCollection CoverImages,
        string Description,
        Guid CategoryId) : ICommand;

    public class UpdateClinicServiceBody
    {
        public Guid Id { get; set; }
        public string ClinicId { get; set; }
        public string Name { get; set; }
        public string? IndexCoverImagesChange { get; set; }
        public IFormFileCollection? CoverImages { get; set; }
        public string Description { get; set; }
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
        Guid CategoryId) : ICommand;

    public record DeleteClinicServiceCommand(Guid Id) : ICommand;
}