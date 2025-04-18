using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
/// <summary>
///     Handler for processing service booking deposits
/// </summary>
internal sealed class ProcessServiceBookingDepositCommandHandler(
    IRepositoryBase<User, Guid> userRepository,
    IRepositoryBase<Service, Guid> serviceRepository,
    IRepositoryBase<Order, Guid> orderRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.ProcessServiceBookingDepositCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.ProcessServiceBookingDepositCommand request,
        CancellationToken cancellationToken)
    {
        // Validate user exists and has permission
        var user = await userRepository.FindByIdAsync(request.CustomerId, cancellationToken);
        if (user == null) return Result.Failure(new Error("404", "User not found"));

        // Validate service exists
        var service = await serviceRepository.FindByIdAsync(request.ServiceId, cancellationToken);
        if (service == null) return Result.Failure(new Error("404", "Service not found"));

        // Validate order exists
        var order = await orderRepository.FindByIdAsync(request.OrderId, cancellationToken);
        if (order == null) return Result.Failure(new Error("404", "Order not found"));

        // Validate user has sufficient balance
        if (user.Balance < request.DepositAmount)
            return Result.Failure(new Error("400", ErrorMessages.Wallet.InsufficientBalance));

        // Create and save the transaction
        var walletTransaction = CreateDepositTransaction(
            request.CustomerId,
            request.OrderId,
            request.DepositAmount,
            request.Description);

        // Deduct the deposit amount from the user's balance
        user.Balance -= request.DepositAmount;
        userRepository.Update(user);

        // Save the transaction
        walletTransactionRepository.Add(walletTransaction);

        return Result.Success(new { TransactionId = walletTransaction.Id });
    }

    /// <summary>
    ///     Creates a new wallet transaction for the service booking deposit
    /// </summary>
    private static WalletTransaction CreateDepositTransaction(
        Guid userId,
        Guid orderId,
        decimal amount,
        string description)
    {
        // Get current time in Vietnam timezone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, vietnamTimeZone);

        return new WalletTransaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = amount,
            TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT,
            Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
            IsMakeBySystem = true,
            Description = description,
            TransactionDate = currentDateTime,
            CreatedOnUtc = DateTimeOffset.UtcNow,
            OrderId = orderId
        };
    }
}