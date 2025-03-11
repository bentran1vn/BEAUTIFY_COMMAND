using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Procedures;

public class Commands
{
    public record CreateProcedureCommand(
        Guid ClinicServiceId,
        string Name,
        string Description,
        int StepIndex,
        IFormFileCollection ProcedureCoverImage,
        IEnumerable<ProcedurePriceType>? ProcedurePriceTypes = null) : ICommand;
    
    public record CreateProcedureBody(
        Guid ClinicServiceId,
        string Name,
        string Description,
        int StepIndex,
        IFormFileCollection ProcedureCoverImage,
        string? ProcedurePriceTypes = null);

    public record ProcedurePriceType(string Name, int Duration, decimal Price);
    
    public record DeleteProcedureCommand(
        Guid Id
        ) : ICommand;
}