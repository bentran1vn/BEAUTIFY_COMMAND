namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Surveys;
internal sealed class CreateSurveyCommandHandler(
    IRepositoryBase<Survey, Guid> surveyRepositoryBase,
    IRepositoryBase<Category, Guid> categoryRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Survey.Commands.CreateSurveyCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Survey.Commands.CreateSurveyCommand request,
        CancellationToken cancellationToken)
    {
        var trimmedName = request.Name.Trim();
        var trimmedDescription = request.Description.Trim();
        var category = await categoryRepositoryBase.FindByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
        {
            return Result.Failure(new Error("404", "Category not found."));
        }

        if (!category.Name.Contains(request.Type, StringComparison.CurrentCultureIgnoreCase))
        {
            return Result.Failure(new Error("400", "Category not belong to survey."));
        }

        var survey = new Survey
        {
            Name = trimmedName,
            Description = trimmedDescription,
            CategoryId = category.Id,
        };
        surveyRepositoryBase.Add(survey);
        return Result.Success();
    }
}