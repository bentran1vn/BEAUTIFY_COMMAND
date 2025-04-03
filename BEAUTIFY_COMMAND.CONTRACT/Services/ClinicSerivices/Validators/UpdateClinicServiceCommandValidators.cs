namespace BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices.Validators;
public class UpdateClinicServiceCommandValidators : AbstractValidator<Commands.UpdateClinicServiceCommand>
{
    public UpdateClinicServiceCommandValidators()
    {
        RuleFor(x => x.ClinicId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Category Name must be at least 5 characters long")
            .MaximumLength(50).WithMessage("Category Name must exceed 50 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Description must be at least 5 characters long");
        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}