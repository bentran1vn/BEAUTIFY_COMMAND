using Microsoft.AspNetCore.Http;

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

    public record ProcedurePriceType(string Name, int Duration, decimal Price,bool IsDefault);

    public record DeleteProcedureCommand(
        Guid Id
    ) : ICommand;
}