namespace BEAUTIFY_COMMAND.CONTRACT.Services.SurveyQuestions;
public static class Commands
{
    public record SurveyQuestionCommand(string Question, int QuestionType, string Option) : ICommand;

    public record SurveyQuestionCommandWithSurveyIdCommand(Guid SurveyId, List<SurveyQuestion> SurveyQuestions)
        : ICommand;

    //Dont use this
    public record SurveyQuestion(string Question, int QuestionType, string Option);
}