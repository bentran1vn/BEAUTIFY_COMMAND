namespace BEAUTIFY_COMMAND.CONTRACT.Services.Categories.Validators;
public class CreateCategoryCommandValidators : AbstractValidator<Commands.CreateCategoryCommand>
{
    public CreateCategoryCommandValidators()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Category Name must be at least 5 characters long")
            .MaximumLength(50).WithMessage("Category Name must exceed 50 characters");
    }
}