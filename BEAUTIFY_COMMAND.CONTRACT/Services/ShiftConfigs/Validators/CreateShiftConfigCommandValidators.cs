namespace BEAUTIFY_COMMAND.CONTRACT.Services.ShiftConfigs.Validators;

public class CreateShiftConfigCommandValidators: AbstractValidator<Commands.CreateShiftConfigCommand>
{
    public CreateShiftConfigCommandValidators()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        
        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("StartTime is required.")
            .LessThanOrEqualTo(x => TimeSpan.Parse("24:00:00"));
        
        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("EndTime is required.")
            .LessThanOrEqualTo(x => TimeSpan.Parse("24:00:00"));
        
        RuleFor(x => x.ClinicId)
            .NotEmpty().WithMessage("ClinicId is required.");
    }
}