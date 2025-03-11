namespace BEAUTIFY_COMMAND.CONTRACT.Services.Survey;
public static class Commands
{
    public record CreateSurveyCommand(string Name, string Description,string Type, Guid CategoryId) : ICommand;
    
}

