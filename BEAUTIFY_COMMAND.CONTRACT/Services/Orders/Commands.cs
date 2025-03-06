using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Orders;
public static class Commands
{
    public record CustomerOrderServiceCommand(List<Guid> ProcedureIds) : ICommand;
    
}