using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Surveys;
internal sealed class CreateSurveyCommandHandler(
    IRepositoryBase<Survey, Guid> surveyRepositoryBase,
    IRepositoryBase<Category, Guid> categoryRepositoryBase,
    IRepositoryBase<SurveyQuestion, Guid> surveyQuestionRepositoryBase,
    IRepositoryBase<SurveyQuestionOption, Guid> surveyQuestionOptionRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Survey.Commands.CreateSurveyCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Survey.Commands.CreateSurveyCommand request,
        CancellationToken cancellationToken)
    {
        var trimmedName = request.Survey.Name.Trim();
        var trimmedDescription = request.Survey.Description.Trim();
        var category = await categoryRepositoryBase.FindByIdAsync(request.Survey.CategoryId, cancellationToken);
        if (category == null) return Result.Failure(new Error("404", "Category not found."));

        if (!category.Name.Contains(request.Survey.Type, StringComparison.CurrentCultureIgnoreCase))
            return Result.Failure(new Error("400", "Category not belong to survey."));

        var survey = new Survey
        {
            Id = Guid.NewGuid(),
            Name = trimmedName,
            Description = trimmedDescription,
            CategoryId = category.Id
        };
        var questions = new List<SurveyQuestion>();
        var questionOptions = new List<SurveyQuestionOption>();
        foreach (var surveyQuestion in request.SurveyQuestion)
        {
            var questionType = surveyQuestion.QuestionType switch
            {
                1 => Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                2 => Constant.SurveyQuestionType.SINGLE_CHOICE,
                _ => Constant.SurveyQuestionType.TEXT
            };
            var question = new SurveyQuestion
            {
                Id = Guid.NewGuid(),
                Question = surveyQuestion.Question,
                QuestionType = questionType,
                SurveyId = survey.Id
            };
            questions.Add(question);
            var questionOption = new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                Option = surveyQuestion.Option,
                SurveyQuestionId = question.Id
            };
            questionOptions.Add(questionOption);
        }

        surveyRepositoryBase.Add(survey);
        surveyQuestionRepositoryBase.AddRange(questions);
        surveyQuestionOptionRepositoryBase.AddRange(questionOptions);
        return Result.Success();
    }
}