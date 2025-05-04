namespace BEAUTIFY_COMMAND.CONTRACT.Services.Events.Validators;

public class CreateEventValidators: AbstractValidator<Commands.CreateEventCommand>
{
    public CreateEventValidators()
    {
        RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage("Image is required");
        
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
        
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Start date is required");
    }
}