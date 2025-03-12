namespace BEAUTIFY_COMMAND.CONTRACT.Services.Answer;
public static class Commands
{
    public record CustomerAnswerSurveyCommand(Guid SurveyId, List<SurveyAnswer> SurveyAnswers) : ICommand;


    public sealed record DetermineSurveyAnswerCommand(Guid SurveyId, List<SurveyAnswer> SurveyAnswers) : ICommand;
    public record SurveyAnswer(string Answer, Guid SurveyQuestionId);
}