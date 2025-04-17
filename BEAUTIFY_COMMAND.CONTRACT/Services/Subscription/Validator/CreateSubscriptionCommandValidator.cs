namespace BEAUTIFY_COMMAND.CONTRACT.Services.Subscription.Validator;
internal sealed class CreateSubscriptionCommandValidator : AbstractValidator<Commands.CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
            .MaximumLength(50).WithMessage("Name must exceed 50 characters");


        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(10).WithMessage("Description must be at least 10 characters long")
            .MaximumLength(200).WithMessage("Description must exceed 200 characters");

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(1000).WithMessage("Price must be greater than 1000");


        RuleFor(x => x.Duration)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Duration must be greater than 0");

        RuleFor(x => x.PriceLiveStreamAddition)
            .NotEmpty()
            .GreaterThan(1000).WithMessage("Price must be greater than 1000");

        RuleFor(x => x.PriceLiveStreamAddition)
            .NotEmpty()
            .GreaterThan(1000).WithMessage("Price must be greater than 1000");
    }
}