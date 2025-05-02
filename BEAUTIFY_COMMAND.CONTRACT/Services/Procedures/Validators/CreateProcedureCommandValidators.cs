namespace BEAUTIFY_COMMAND.CONTRACT.Services.Procedures.Validators;
public class CreateProcedureCommandValidators : AbstractValidator<Commands.CreateProcedureCommand>
{
    public CreateProcedureCommandValidators()
    {
        RuleFor(x => x.ClinicServiceId).NotEmpty();

        // RuleFor(x => x.StepIndex).NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Clinic Name must be at least 2 characters long");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(5).WithMessage("Clinic Name must be at least 2 characters long");

        RuleForEach(x => x.ProcedurePriceTypes)
            .SetValidator(new ProcedurePriceTypeValidator());
    }
}

public class ProcedurePriceTypeValidator : AbstractValidator<Commands.ProcedurePriceType>
{
    public ProcedurePriceTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Procedure Price Type Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

        RuleFor(x => x.Duration)
            .GreaterThan(0).WithMessage("Duration is required.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must not negative.");
    }
}