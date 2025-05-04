namespace BEAUTIFY_COMMAND.CONTRACT.Services.Events.Validators;

public class DeleteEventValidators: AbstractValidator<Commands.DeleteEvent>
{
    public DeleteEventValidators()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}