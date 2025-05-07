namespace BEAUTIFY_COMMAND.CONTRACT.Services.Configs;
public static class Command
{
    public record CreateConfigCommand(string Key, string Value) : ICommand;

    public record UpdateConfigCommand(Guid Id, string Value) : ICommand;
}