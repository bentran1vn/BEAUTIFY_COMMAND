using BEAUTIFY_COMMAND.APPLICATION.Hub;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.AspNetCore.SignalR;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
public sealed class TriggerFromHookCommandHandler(
    IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository,
    IHubContext<PaymentHub> hubContext,
    IRepositoryBase<Order, Guid> orderRepository)
    : ICommandHandler<CONTRACT.Services.Payments.Commands.TriggerFromHookCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await ValidateAndGetTransactionAsync(request, cancellationToken);
        if (transaction is null)
            return Result.Failure(new Error("404", "Transaction not found"));

        if (!ValidateTransaction(request, transaction, out var transactionError))
            return Result.Failure(transactionError);

        transaction.Status = 1;

        switch (request.Type)
        {
            case 0:
                await NotifyClientsAsync(transaction.Id.ToString(), "ReceivePaymentStatus", true, cancellationToken);
                return Result.Success("Transaction processed successfully.");
            case 1:
            {
                var order = await ValidateAndUpdateOrderAsync(request, transaction, cancellationToken);
                if (order is null)
                    return Result.Failure(new Error("404", "Order not found or invalid"));

                await NotifyClientsAsync(order.Id.ToString(), Constant.OrderStatus.ORDER_COMPLETED, true,
                    cancellationToken);
                return Result.Success("Order completed successfully.");
            }
            default:
                return Result.Failure(new Error("400", "Invalid request type"));
        }
    }

    private async Task<SystemTransaction?> ValidateAndGetTransactionAsync(
        CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request, CancellationToken cancellationToken) =>
        await systemTransactionRepository.FindByIdAsync(request.Id, cancellationToken);

    private static bool ValidateTransaction(CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        SystemTransaction transaction, out Error error)
    {
        if (transaction.IsDeleted)
        {
            error = new Error("404", "Transaction not found");
            return false;
        }

        if (transaction.Status != 0)
        {
            error = new Error("400", "Transaction already handled");
            return false;
        }

        if (transaction.Amount != request.TransferAmount)
        {
            error = new Error("422", "Transaction amount invalid");
            return false;
        }

        if (transaction.TransactionDate > DateTimeOffset.Now)
        {
            error = new Error("400", "Transaction date invalid");
            return false;
        }

        error = null!;
        return true;
    }

    private async Task<Order?> ValidateAndUpdateOrderAsync(
        CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        SystemTransaction transaction,
        CancellationToken cancellationToken)
    {
        if (!transaction.OrderId.HasValue)
            return null;

        var order = await orderRepository.FindByIdAsync(transaction.OrderId.Value, cancellationToken);
        if (order == null || order.IsDeleted)
            return null;

        if (order.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return null;

        if (order.FinalAmount != request.TransferAmount)
            return null;

        order.Status = Constant.OrderStatus.ORDER_COMPLETED;
        return order;
    }

    private async Task NotifyClientsAsync(string groupId, string method, bool status,
        CancellationToken cancellationToken) =>
        await hubContext.Clients.Group(groupId).SendAsync(method, status, cancellationToken);
}