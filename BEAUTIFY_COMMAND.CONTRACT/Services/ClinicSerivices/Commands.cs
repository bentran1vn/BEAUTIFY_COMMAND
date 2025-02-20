using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices;

public class Commands
{
    public record CreateClinicServiceCommand(
        Guid[] ClinicId,
        string Name,
        decimal Price,
        IFormFileCollection CoverImages,
        string Description,
        IFormFileCollection DescriptionImages,
        Guid CategoryId) : ICommand;
    
    public record DeleteClinicServiceCommand(Guid Id): ICommand;
}