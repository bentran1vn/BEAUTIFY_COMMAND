using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.SurveyQuestions;
internal sealed class CreateSurveyQuestionCommandHandler(
    IRepositoryBase<SurveyQuestion, Guid> surveyQuestionRepositoryBase,
    IRepositoryBase<Survey, Guid> surveyRepositoryBase,
    IRepositoryBase<SurveyQuestionOption, Guid> surveyQuestionOptionRepositoryBase)
    : ICommandHandler<CONTRACT.Services.SurveyQuestions.Commands.SurveyQuestionCommandWithSurveyIdCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.SurveyQuestions.Commands.SurveyQuestionCommandWithSurveyIdCommand request,
        CancellationToken cancellationToken)
    {
        if (request.SurveyQuestions.FirstOrDefault().Question.Equals("string") ||
            request.SurveyQuestions.FirstOrDefault().QuestionType == 0 ||
            request.SurveyQuestions.FirstOrDefault().Option.Equals("string"))
        {
            return Result.Failure(new Error("400", "Invalid survey question."));
        }

        var survey = await surveyRepositoryBase.FindByIdAsync(request.SurveyId, cancellationToken);
        if (survey == null)
        {
            return Result.Failure(new Error("404", "Survey not found."));
        }

        if (request.SurveyQuestions.Count == 0)
        {
            return Result.Failure(new Error("400", "No survey questions provided."));
        }

        var surveyQuestions = request.SurveyQuestions
            .Select(x => new SurveyQuestion
            {
                Id = Guid.NewGuid(),
                Question = x.Question,
                QuestionType = x.QuestionType switch
                {
                    1 => Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    2 => Constant.SurveyQuestionType.SINGLE_CHOICE,
                    _ => Constant.SurveyQuestionType.TEXT
                },
                SurveyId = survey.Id
            })
            .ToList();

        var surveyQuestionOptions = request.SurveyQuestions
            .Select(x => new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                Option = x.Option,
                SurveyQuestionId = surveyQuestions
                    .FirstOrDefault(q => q.Question == x.Question)?.Id ?? Guid.Empty
            })
            .ToList();

        surveyQuestionRepositoryBase.AddRange(surveyQuestions);
        surveyQuestionOptionRepositoryBase.AddRange(surveyQuestionOptions);
        return Result.Success();
    }
}