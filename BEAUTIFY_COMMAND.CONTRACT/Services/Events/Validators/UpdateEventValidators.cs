namespace BEAUTIFY_COMMAND.CONTRACT.Services.Events.Validators;

public class UpdateEventValidators: AbstractValidator<Commands.UpdateEventCommand>
{
    public UpdateEventValidators()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        
        RuleFor(x => x.ClinicId)
            .NotEmpty()
            .WithMessage("ClinicId is required");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required");
    }
}