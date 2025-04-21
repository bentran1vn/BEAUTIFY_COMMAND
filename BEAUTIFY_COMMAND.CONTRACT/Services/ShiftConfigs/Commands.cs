namespace BEAUTIFY_COMMAND.CONTRACT.Services.ShiftConfigs;

public static class Commands
{
    public record CreateShiftConfigCommand(
        string Name, string? Note, TimeSpan StartTime, TimeSpan EndTime,
        Guid ClinicId
    ) : ICommand;
    
    public record CreateShiftConfigBody(
        string Name, string Note, TimeSpan StartTime, TimeSpan EndTime
    );
    
    public record UpdateShiftConfigBody(
        Guid Id, string Name, string Note, TimeSpan StartTime, TimeSpan EndTime
    );
    
    public record UpdateShiftConfigCommand(
        Guid Id, string Name, string? Note, TimeSpan StartTime, TimeSpan EndTime,
        Guid ClinicId
    ) : ICommand;
}