using FluentValidation;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.DoctorCertificate.Validator;
internal sealed class UpdateDoctorCertificateValidator : AbstractValidator<Commands.UpdateCommand>
{
    public UpdateDoctorCertificateValidator()
    {
        RuleFor(x => x.CertificateName)
            .NotEmpty()
            .MinimumLength(2).WithMessage("Certificate Name must be at least 2 characters long")
            .MaximumLength(50).WithMessage("Certificate Name must exceed 50 characters");

        RuleFor(x => x.ExpiryDate)
            .NotEmpty()
            .WithMessage("Expiry Date is required")
            .GreaterThan(DateTimeOffset.Now)
            .WithMessage("Expiry Date must be greater than current date");

        RuleFor(x => x.Note)
            .MaximumLength(200).WithMessage("Note must exceed 200 characters");
    }
}