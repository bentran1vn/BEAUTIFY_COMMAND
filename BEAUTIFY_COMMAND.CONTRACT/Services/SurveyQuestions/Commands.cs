namespace BEAUTIFY_COMMAND.CONTRACT.Services.SurveyQuestions;
public static class Commands
{
    public record CreateSurveyQuestionCommand(string Question, int QuestionType, Guid SurveyId) : ICommand;
}