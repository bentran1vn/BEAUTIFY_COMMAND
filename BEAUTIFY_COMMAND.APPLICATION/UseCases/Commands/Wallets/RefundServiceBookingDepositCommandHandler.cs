namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
/// <summary>
/// Handler for refunding service booking deposits after the first meeting
/// </summary>
internal sealed class RefundServiceBookingDepositCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<Order, Guid> orderRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.RefundServiceBookingDepositCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.RefundServiceBookingDepositCommand request,
        CancellationToken cancellationToken)
    {
        // Validate user exists
        var user = await userRepository.FindByIdAsync(request.CustomerId, cancellationToken);
        if (user == null)
        {
            return Result.Failure(new Error("404", "User not found"));
        }

        // Validate order exists
        var order = await orderRepository.FindByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
        {
            return Result.Failure(new Error("404", "Order not found"));
        }

        // Validate original deposit transaction exists
        var originalTransaction = await walletTransactionRepository.FindByIdAsync(
            request.WalletTransactionId, cancellationToken);

        if (originalTransaction == null)
        {
            return Result.Failure(new Error("404", "Original deposit transaction not found"));
        }

        // Validate transaction is a service deposit
        if (originalTransaction.TransactionType != Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT)
        {
            return Result.Failure(new Error("400", "Transaction is not a service deposit"));
        }

        // Create and save the refund transaction
        var refundTransaction = CreateRefundTransaction(
            request.CustomerId,
            request.OrderId,
            originalTransaction.Amount,
            request.Description,
            request.WalletTransactionId);

        // Add the deposit amount back to the user's balance
        user.Balance += originalTransaction.Amount;
        userRepository.Update(user);

        // Save the transaction
        walletTransactionRepository.Add(refundTransaction);

        return Result.Success(new { TransactionId = refundTransaction.Id });
    }

    /// <summary>
    /// Creates a new wallet transaction for the service booking deposit refund
    /// </summary>
    private static WalletTransaction CreateRefundTransaction(
        Guid userId,
        Guid orderId,
        decimal amount,
        string description,
        Guid originalTransactionId)
    {
        // Get current time in Vietnam timezone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, vietnamTimeZone);

        return new WalletTransaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = amount,
            TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT_REFUND,
            Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
            IsMakeBySystem = true,
            Description = description,
            TransactionDate = currentDateTime,
            CreatedOnUtc = DateTimeOffset.UtcNow,
            OrderId = orderId,
            RelatedTransactionId = originalTransactionId
        };
    }
}