namespace BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices.Validators;
public class CreateClinicServiceCommandValidators : AbstractValidator<Commands.CreateClinicServiceCommand>
{
    public CreateClinicServiceCommandValidators()
    {
        RuleFor(x => x.ClinicId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Service Name must be at least 5 characters long");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(5).WithMessage(" Description must be at least 5 characters long");

        RuleFor(x => x.CoverImages)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}