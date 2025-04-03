using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Procedures;
public class Commands
{
    public record CreateProcedureCommand(
        Guid ClinicServiceId,
        string Name,
        string Description,
        int? StepIndex,
        IEnumerable<ProcedurePriceType> ProcedurePriceTypes) : ICommand;
    
    public record UpdateProcedureCommand(
        Guid ServiceId,
        Guid ProcedureId,
        string Name,
        string Description,
        int StepIndex,
        IEnumerable<ProcedurePriceType> ProcedurePriceTypes) : ICommand;

    public record ProcedurePriceType(
        string Name, int Duration, decimal Price,bool IsDefault);

    public record DeleteProcedureCommand(
        Guid Id
    ) : ICommand;
}