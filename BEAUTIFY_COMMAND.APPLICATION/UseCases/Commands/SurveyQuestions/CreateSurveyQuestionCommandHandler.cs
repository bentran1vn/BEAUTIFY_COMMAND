using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.SurveyQuestions;
internal sealed class CreateSurveyQuestionCommandHandler(
    IRepositoryBase<SurveyQuestion, Guid> surveyQuestionRepositoryBase,
    IRepositoryBase<Survey, Guid> surveyRepositoryBase)
    : ICommandHandler<CONTRACT.Services.SurveyQuestions.Commands.CreateSurveyQuestionCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.SurveyQuestions.Commands.CreateSurveyQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var survey = await surveyRepositoryBase.FindByIdAsync(request.SurveyId, cancellationToken);
        if (survey == null)
        {
            return Result.Failure(new Error("404", "Survey not found."));
        }

        var questionType = request.QuestionType switch
        {
            1 => Constant.SurveyQuestionType.MULTIPLE_CHOICE,
            2 => Constant.SurveyQuestionType.SINGLE_CHOICE,
            _ => Constant.SurveyQuestionType.TEXT,
        };
        var surveyQuestion = new SurveyQuestion
        {
            Question = request.Question,
            QuestionType = questionType,
            SurveyId = survey.Id,
        };
        surveyQuestionRepositoryBase.Add(surveyQuestion);
        return Result.Success();
    }
}