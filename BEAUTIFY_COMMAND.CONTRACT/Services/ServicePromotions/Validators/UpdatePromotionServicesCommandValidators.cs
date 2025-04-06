namespace BEAUTIFY_COMMAND.CONTRACT.Services.ServicePromotions.Validators;
public class UpdatePromotionServicesCommandValidators : AbstractValidator<Commands.UpdatePromotionServicesCommand>
{
    public UpdatePromotionServicesCommandValidators()
    {
        RuleFor(x => x.PromotionId)
            .NotEmpty().WithMessage("Promotion ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Promotion Name is required.")
            .MinimumLength(5).WithMessage("Promotion Name must be at least 5 characters long.")
            .MaximumLength(300).WithMessage("Promotion Name must not exceed 300 characters.");

        RuleFor(x => x.DiscountPercent)
            .NotEmpty().WithMessage("Discount Percent is required.")
            .GreaterThan(0).WithMessage("Discount Percent must be greater than 0.")
            .LessThan(100).WithMessage("Discount Percent must be less than 100.");

        RuleFor(x => x.StartDay)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(x => x.StartDay).WithMessage("End date must be after the start date.");
    }
}