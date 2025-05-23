namespace BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices.Validators;
public class UpdateClinicServiceCommandValidators : AbstractValidator<Commands.UpdateClinicServiceCommand>
{
    public UpdateClinicServiceCommandValidators()
    {
        RuleFor(x => x.ClinicId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Name must be at least 5 characters long");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Description must be at least 5 characters long");
        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}