namespace BEAUTIFY_COMMAND.CONTRACT.Services.Wallets.Validators;
internal sealed class CustomerTopUpWalletCommandValidator : AbstractValidator<Commands.CustomerTopUpWalletCommand>
{
    public CustomerTopUpWalletCommandValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Amount is required.")
            .GreaterThan(1999)
            .WithMessage("Amount must be greater than 2000.");
    }
}