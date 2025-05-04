namespace BEAUTIFY_COMMAND.CONTRACT.Services.Followers.Validatos;

public class FollowCommandValidators: AbstractValidator<Commands.FollowCommand>
{
    public FollowCommandValidators()
    {
        RuleFor(x => x.ClinicId)
            .NotEmpty()
            .WithMessage("ClinicId is required");
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");
        
    }
}
