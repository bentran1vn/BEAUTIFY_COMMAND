namespace BEAUTIFY_COMMAND.CONTRACT.Services.Users.Validators;
public class UpdateUserProfileCommandValidator : AbstractValidator<Commands.UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(14).WithMessage("Phone number must not exceed 14 characters")
            .Matches(@"^[0-9]*$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("Phone number must contain only digits");

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City must not exceed 100 characters");

        RuleFor(x => x.District)
            .MaximumLength(100).WithMessage("District must not exceed 100 characters");

        RuleFor(x => x.Ward)
            .MaximumLength(100).WithMessage("Ward must not exceed 100 characters");

        RuleFor(x => x.Address)
            .MaximumLength(100).WithMessage("Address must not exceed 100 characters");

        RuleFor(x => x.DateOfBirth)
            .Must(BeValidDateOfBirth).WithMessage("Date of birth must be in the past");
    }

    private bool BeValidDateOfBirth(DateOnly? dateOfBirth)
    {
        if (dateOfBirth == null)
            return true;

        return dateOfBirth < DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
