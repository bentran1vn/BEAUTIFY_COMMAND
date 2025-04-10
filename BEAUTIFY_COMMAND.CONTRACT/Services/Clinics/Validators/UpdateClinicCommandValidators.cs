namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics.Validators;
public class UpdateClinicCommandValidators : AbstractValidator<Commands.UpdateClinicCommand>
{
    public UpdateClinicCommandValidators()
    {
        RuleFor(x => x.ClinicId)
            .NotNull().NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(5)
            .MaximumLength(30)
            .When(x => !string.IsNullOrEmpty(x.Name))
            .WithMessage("Clinic Name must be between 5 and 30 characters long.");
        
    }
}