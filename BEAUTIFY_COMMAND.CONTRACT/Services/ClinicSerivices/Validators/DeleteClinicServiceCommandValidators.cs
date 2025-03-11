namespace BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices.Validators;

public class DeleteClinicServiceCommandValidators : AbstractValidator<Commands.DeleteClinicServiceCommand>
{
    public DeleteClinicServiceCommandValidators()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}