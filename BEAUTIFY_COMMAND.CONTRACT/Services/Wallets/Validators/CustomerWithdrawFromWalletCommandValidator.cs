namespace BEAUTIFY_COMMAND.CONTRACT.Services.Wallets.Validators;
internal sealed class
    CustomerWithdrawFromWalletCommandValidator : AbstractValidator<Commands.CustomerWithdrawFromWalletCommand>
{
    public CustomerWithdrawFromWalletCommandValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Amount is required.")
            .GreaterThan(1999)
            .WithMessage("Amount must be greater than 2000.");

        RuleFor(x => x.BankName)
            .NotEmpty()
            .WithMessage("Bank name is required.")
            .MaximumLength(100)
            .WithMessage("Bank name must not exceed 100 characters.");

        RuleFor(x => x.BankAccountNumber)
            .NotEmpty()
            .WithMessage("Bank account number is required.")
            .MaximumLength(20)
            .WithMessage("Bank account number must not exceed 20 characters.");

        RuleFor(x => x.AccountHolderName)
            .NotEmpty()
            .WithMessage("Account holder name is required.")
            .MaximumLength(100)
            .WithMessage("Account holder name must not exceed 100 characters.");
    }
}