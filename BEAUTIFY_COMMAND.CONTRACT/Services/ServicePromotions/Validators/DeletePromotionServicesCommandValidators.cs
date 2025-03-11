namespace BEAUTIFY_COMMAND.CONTRACT.Services.ServicePromotions.Validators;

public class DeletePromotionServicesCommandValidators: AbstractValidator<Commands.DeletePromotionServicesCommand>
{
    public DeletePromotionServicesCommandValidators()
    {
        RuleFor(x => x.PromotionId)
            .NotEmpty().WithMessage("Promotion ID is required.");
    }
}