using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
/// <summary>
///     Handler for customer withdrawal requests
/// </summary>
internal sealed class CustomerWithdrawFromWalletCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.CustomerWithdrawFromWalletCommand>
{
    //todo : move to config
    private const decimal MINIMUM_WITHDRAWAL_AMOUNT = 2000;

    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.CustomerWithdrawFromWalletCommand request,
        CancellationToken cancellationToken)
    {
        // Validate user exists and has permission


        // Get the user with a single database call
        var user = await userRepository.FindByIdAsync(currentUserService.UserId.Value, cancellationToken);
        if (user == null) return Result.Failure(new Error("404", "User not found"));

        // Validate request parameters
        var validationResult = ValidateWithdrawalRequest(user, request.Amount);
        if (validationResult.IsFailure) return validationResult;

        // Create and save the transaction
        var walletTransaction = CreateWalletTransaction(
            currentUserService.UserId.Value,
            request.Amount,
            $"Withdrawal to {request.BankName} - {request.BankAccountNumber} - {request.AccountHolderName}");

        walletTransactionRepository.Add(walletTransaction);
       

        return Result.Success();
    }

    /// <summary>
    ///     Validates the withdrawal request parameters
    /// </summary>
    private static Result ValidateWithdrawalRequest(User user, decimal amount)
    {
        // Verify the amount is valid
        if (amount < MINIMUM_WITHDRAWAL_AMOUNT)
            return Result.Failure(new Error("400", ErrorMessages.Clinic.AmountMustBeGreaterThan2000));

        // Verify the user has sufficient balance
        return user.Balance < amount
            ? Result.Failure(new Error("400", ErrorMessages.Wallet.InsufficientBalance))
            : Result.Success();
    }

    /// <summary>
    ///     Creates a new wallet transaction for the withdrawal request
    /// </summary>
    private static WalletTransaction CreateWalletTransaction(Guid userId, decimal amount, string description)
    {
        // Get current time in Vietnam timezone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, vietnamTimeZone);

        return new WalletTransaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = amount,
            TransactionType = Constant.WalletConstants.TransactionType.WITHDRAWAL,
            Status = Constant.WalletConstants.TransactionStatus.WAITING_FOR_PAYMENT,
            IsMakeBySystem = true,
            Description = description,
            TransactionDate = currentDateTime,
            CreatedOnUtc = DateTimeOffset.UtcNow
        };
    }
}