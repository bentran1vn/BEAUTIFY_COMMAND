using FluentValidation;

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
        
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Invalid phone number format");

        RuleFor(x => x.Address)
            .MinimumLength(10)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Address))
            .WithMessage("Address must be between 10 and 100 characters long.");
        
        // RuleFor(x => x.ProfilePicture)
        //     .Must(file => file.Length > 0)
        //     .When(file => file != null)
        //     .WithMessage("Profile Picture must be a valid file if provided.");
    }
}