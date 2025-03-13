namespace BEAUTIFY_COMMAND.CONTRACT.Services.Categories.Validators;
public class DeleteCategoryCommandValidators : AbstractValidator<Commands.DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidators()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
    }
}