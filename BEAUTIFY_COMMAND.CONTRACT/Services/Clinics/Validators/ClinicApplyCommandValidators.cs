namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics.Validators;
public class ClinicApplyCommandValidators : AbstractValidator<Commands.ClinicApplyCommand>
{
    public ClinicApplyCommandValidators()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Clinic Name must be at least 5 characters")
            .MaximumLength(30).WithMessage("Clinic Name must exceed 30 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email format");

        //validate tax code is number only and can only accept hyphen
        RuleFor(x => x.TaxCode)
            .NotEmpty()
            .Matches(@"^[0-9-]+$").WithMessage("Tax code must be a number only");


        /*RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format");*/

        //rule for phone number to contain only number and has 10 digits and start with 0
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^0[0-9]{9}$").WithMessage("Invalid phone number format");


        RuleFor(x => x.TaxCode)
            .NotEmpty().WithMessage("Tax code is required");

        // RuleFor(x => x.BusinessLicense)
        //     .NotEmpty().WithMessage("Business License URL is required");
        //
        // RuleFor(x => x.OperatingLicense)
        //     .NotEmpty().WithMessage("Operating License URL is required");
        //
        // RuleFor(x => x.OperatingLicenseExpiryDate)
        //     .NotEmpty().WithMessage("Operating License Expiry Date is required")
        //     .Must(BeAValidDate).WithMessage("Invalid date format");
        //
        // RuleFor(x => x.ProfilePictureUrl)
        //     .NotEmpty().WithMessage("Profile Picture URL is required");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParse(date, out _);
    }
}