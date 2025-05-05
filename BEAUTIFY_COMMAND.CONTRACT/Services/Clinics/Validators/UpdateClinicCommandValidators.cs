namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics.Validators;
public class UpdateClinicCommandValidators : AbstractValidator<Commands.UpdateClinicCommand>
{
    public UpdateClinicCommandValidators()
    {
       /* RuleFor(x => x.Name)
            
            .MinimumLength(5).WithMessage("Clinic Name must be at least 2 word long")
            .MaximumLength(30).WithMessage("Clinic Name must exceed 30 characters");

        RuleFor(x => x.PhoneNumber)
            
            .Matches(@"^0[0-9]{9}$").WithMessage("Invalid phone number format");*/
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }
}