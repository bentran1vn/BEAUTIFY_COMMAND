using FluentValidation;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Categories.Validators;

public class UpdateCategoryCommandValidators: AbstractValidator<Commands.UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidators()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Category Name must be at least 5 characters long")
            .MaximumLength(50).WithMessage("Category Name must exceed 50 characters");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Category Description must be at least 5 characters long")
            .MaximumLength(100).WithMessage("Category Description must exceed 100 characters");
        
    }
}