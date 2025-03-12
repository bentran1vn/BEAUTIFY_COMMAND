namespace BEAUTIFY_COMMAND.CONTRACT.Services.Survey;
public static class Commands
{
    public record CreateSurveyCommand(SurveyCommand Survey, List<SurveyQuestions.Commands.SurveyQuestionCommand> SurveyQuestion) : ICommand;


    public record SurveyCommand(string Name, string Description, string Type, Guid CategoryId);

   
}