using BEAUTIFY_COMMAND.APPLICATION.Hub;
using Microsoft.AspNetCore.SignalR;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
public class TriggerFromHookCommandHandler(
    IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository,
    IRepositoryBase<ClinicTransaction, Guid> clinicTransactionRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    IHubContext<PaymentHub> hubContext,
    IRepositoryBase<Order, Guid> orderRepository)
    : ICommandHandler<CONTRACT.Services.Payments.Commands.TriggerFromHookCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        CancellationToken cancellationToken)
    {
        // Process different transaction types based on the request Type
        return request.Type switch
        {
            0 => // Subscription transaction
                await HandleSubscriptionTransaction(request, cancellationToken),
            1 => // Clinic transaction
                await HandleClinicTransaction(request, cancellationToken),
            2 => // Wallet transaction
                await HandleWalletTransaction(request, cancellationToken),
            3 => // Withdrawal transaction
                await HandleWithdrawalTransaction(request, cancellationToken),
            _ => Result.Failure(new Error("400", "Invalid transaction type"))
        };
    }

    private async Task<Result> HandleSubscriptionTransaction(
        CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        CancellationToken cancellationToken)
    {
        // Fetch transaction with subscription package
        var transaction = await systemTransactionRepository.FindByIdAsync(
            request.Id,
            cancellationToken,
            x => x.SubscriptionPackage);

        // Validate transaction exists
        if (transaction == null || transaction.IsDeleted)
            return Result.Failure(new Error("404", "Transaction not found"));

        // Validate transaction status
        if (transaction.Status != 0)
            return Result.Failure(new Error("400", "Transaction already handled"));

        // Validate transaction amount
        if (transaction.Amount != request.TransferAmount)
            return Result.Failure(new Error("422", "Transaction Amount invalid"));

        // Validate subscription package price
        if (request.TransferAmount != transaction.SubscriptionPackage.Price)
        {
            await hubContext.Clients.Group(transaction.Id.ToString())
                .SendAsync("SubscriptionPriceChanged", false, cancellationToken);
            return Result.Success("Subscription price changed notification sent.");
        }

        // Validate transaction date
        if (transaction.TransactionDate > DateTimeOffset.Now)
            return Result.Failure(new Error("400", "Transaction Date invalid"));

        // Update transaction status to completed (1)
        // The background job will look for transactions with status 1 and SubscriptionPackageId not null
        // to send confirmation emails
        transaction.Status = 1;

        // Notify clients about successful payment
        await hubContext.Clients.Group(transaction.Id.ToString())
            .SendAsync("ReceivePaymentStatus", true, cancellationToken);

        return Result.Success("Subscription transaction processed successfully.");
    }

    private async Task<Result> HandleClinicTransaction(
        CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        CancellationToken cancellationToken)
    {
        // Fetch clinic transaction
        var transaction = await clinicTransactionRepository.FindByIdAsync(request.Id, cancellationToken);

        // Validate transaction exists
        if (transaction == null || transaction.IsDeleted)
            return Result.Failure(new Error("404", "Transaction not found"));

        // Validate transaction status
        if (transaction.Status != Constant.OrderStatus.ORDER_PENDING)
            return Result.Failure(new Error("400", "Transaction already handled"));

        // Validate transaction amount
        if (transaction.Amount != request.TransferAmount)
            return Result.Failure(new Error("422", "Transaction Amount invalid"));

        // Validate transaction date
        if (transaction.TransactionDate > DateTimeOffset.Now)
            return Result.Failure(new Error("400", "Transaction Date invalid"));

        // Update transaction status to completed
        transaction.Status = Constant.OrderStatus.ORDER_COMPLETED;

        // Validate and update associated order
        if (!transaction.OrderId.HasValue)
            return Result.Failure(new Error("400", "Transaction has no associated order"));

        var order = await orderRepository.FindByIdAsync(transaction.OrderId.Value, cancellationToken);

        if (order == null || order.IsDeleted)
            return Result.Failure(new Error("404", "Order not found"));

        if (order.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Order already completed"));

        if (order.FinalAmount != request.TransferAmount)
            return Result.Failure(new Error("422", "Order Amount invalid"));

        // Update order status to completed
        order.Status = Constant.OrderStatus.ORDER_COMPLETED;

        // Notify clients about successful payment
        await hubContext.Clients.Group(transaction.Id.ToString())
            .SendAsync("ReceivePaymentStatus", true, cancellationToken);

        return Result.Success("Clinic transaction processed successfully.");
    }

    private async Task<Result> HandleWalletTransaction(
        CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        CancellationToken cancellationToken)
    {
        // Fetch wallet transaction with user
        var transaction = await walletTransactionRepository.FindByIdAsync(
            request.Id,
            cancellationToken,
            x => x.User);

        // Validate transaction exists
        if (transaction == null || transaction.IsDeleted)
            return Result.Failure(new Error("404", "Transaction not found"));

        // Validate transaction status
        if (transaction.Status != Constant.WalletConstants.TransactionStatus.PENDING)
            return Result.Failure(new Error("400", "Transaction already handled"));

        // Validate transaction amount
        if (transaction.Amount != request.TransferAmount)
            return Result.Failure(new Error("422", "Transaction Amount invalid"));

        // Validate transaction date (must be today)
        if (transaction.TransactionDate.Date != DateTimeOffset.Now.Date)
            return Result.Failure(new Error("400", "Transaction Date invalid"));

        // Update transaction status to completed
        transaction.Status = Constant.WalletConstants.TransactionStatus.COMPLETED;

        // Ensure user exists
        if (transaction.User == null)
            return Result.Failure(new Error("404", "User not found for this transaction"));

        // Update user balance
        transaction.User.Balance += transaction.Amount;

        // Notify clients about successful payment
        await hubContext.Clients.Group(transaction.Id.ToString())
            .SendAsync("ReceivePaymentStatus", true, cancellationToken);

        return Result.Success("Wallet transaction processed successfully.");
    }

    private async Task<Result> HandleWithdrawalTransaction(
        CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        CancellationToken cancellationToken)
    {
        // Fetch withdrawal transaction with related entities
        var transaction = await walletTransactionRepository.FindByIdAsync(
            request.Id,
            cancellationToken,
            x => x.User,
            x => x.Clinic);

        // Validate transaction exists
        if (transaction == null || transaction.IsDeleted)
            return Result.Failure(new Error("404", "Transaction not found"));

        // Validate transaction status
        if (transaction.Status != Constant.WalletConstants.TransactionStatus.WAITING_FOR_PAYMENT)
            return Result.Failure(new Error("400", "Transaction already handled"));

        // Validate transaction amount
        if (transaction.Amount != request.TransferAmount)
            return Result.Failure(new Error("422", "Transaction Amount invalid"));

        // Update transaction status to completed
        transaction.Status = Constant.WalletConstants.TransactionStatus.COMPLETED;

        // Handle clinic withdrawal
        if (transaction.Clinic != null)
        {
            // Update clinic balance
            transaction.Clinic.Balance -= transaction.Amount;
        }
        // Handle user withdrawal
        else if (transaction.User != null)
        {
            // Update user balance
            transaction.User.Balance -= transaction.Amount;
        }
        else
        {
            return Result.Failure(new Error("404", "Neither user nor clinic found for this transaction"));
        }

        // Notify clients about successful payment
        await hubContext.Clients.Group(transaction.Id.ToString())
            .SendAsync("ReceivePaymentStatus", true, cancellationToken);

        return Result.Success("Withdrawal transaction processed successfully.");
    }
}