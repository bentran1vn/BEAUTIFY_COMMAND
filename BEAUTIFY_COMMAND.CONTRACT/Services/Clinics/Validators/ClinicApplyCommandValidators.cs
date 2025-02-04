using FluentValidation;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics.Validators;

public class ClinicApplyCommandValidators : AbstractValidator<Commands.ClinicApplyCommand>
{
    public ClinicApplyCommandValidators()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Clinic Name must be at least 2 characters long")
            .MaximumLength(30).WithMessage("Clinic Name must exceed 30 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email format");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format");
        
        RuleFor(x => x.Address)
            .NotEmpty()
            .MinimumLength(10).WithMessage("Address must be at least 10 characters long")
            .MaximumLength(100).WithMessage("Address must exceed 100 characters");
        
        RuleFor(x => x.TaxCode)
            .NotEmpty().WithMessage("Tax code is required");

        RuleFor(x => x.BusinessLicenseUrl)
            .NotEmpty().WithMessage("Business License URL is required")
            .Must(BeAValidUrl).WithMessage("Invalid Business License URL format");

        RuleFor(x => x.OperatingLicenseUrl)
            .NotEmpty().WithMessage("Operating License URL is required")
            .Must(BeAValidUrl).WithMessage("Invalid Operating License URL format");

        RuleFor(x => x.OperatingLicenseExpiryDate)
            .NotEmpty().WithMessage("Operating License Expiry Date is required")
            .Must(BeAValidDate).WithMessage("Invalid date format");

        RuleFor(x => x.ProfilePictureUrl)
            .NotEmpty().WithMessage("Profile Picture URL is required")
            .Must(BeAValidUrl).WithMessage("Invalid Profile Picture URL format");
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