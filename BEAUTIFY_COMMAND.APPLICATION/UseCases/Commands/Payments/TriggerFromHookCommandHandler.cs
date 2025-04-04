using BEAUTIFY_COMMAND.APPLICATION.Hub;
using Microsoft.AspNetCore.SignalR;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
public class TriggerFromHookCommandHandler(
    IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository,
    IHubContext<PaymentHub> hubContext,
    IRepositoryBase<Order, Guid> orderRepository)
    : ICommandHandler<CONTRACT.Services.Payments.Commands.TriggerFromHookCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request,
        CancellationToken cancellationToken)
    {
        switch (request.Type)
        {
            case 0:
            {
                var tran = await systemTransactionRepository.FindByIdAsync(request.Id, cancellationToken);

                if (tran == null || tran.IsDeleted) return Result.Failure(new Error("404", "Transaction not found"));

                if (tran.Status != 0) return Result.Failure(new Error("400", "Transaction already handler"));

                if (tran.Amount != request.TransferAmount)
                    return Result.Failure(new Error("422", "Transaction Amount invalid"));

                if (tran.TransactionDate > DateTimeOffset.Now)
                    return Result.Failure(new Error("400", "Transaction Date invalid"));

                tran.Status = 1;

                await hubContext.Clients.Group(tran.Id.ToString())
                    .SendAsync("ReceivePaymentStatus", true, cancellationToken);
                break;
            }
            case 1:
            {
                var tran = await systemTransactionRepository.FindByIdAsync(request.Id, cancellationToken);

                if (tran == null || tran.IsDeleted) return Result.Failure(new Error("404", "Transaction not found"));

                if (tran.Status != 0) return Result.Failure(new Error("400", "Transaction already handler"));

                if (tran.Amount != request.TransferAmount)
                    return Result.Failure(new Error("422", "Transaction Amount invalid"));

                if (tran.TransactionDate > DateTimeOffset.Now)
                    return Result.Failure(new Error("400", "Transaction Date invalid"));

                tran.Status = 1;
                var order = await orderRepository.FindByIdAsync(tran.OrderId.Value, cancellationToken);
                if (order == null || order.IsDeleted) return Result.Failure(new Error("404", "Order not found"));

                if (order.Status == Constant.OrderStatus.ORDER_COMPLETED)
                    return Result.Failure(new Error("400", "Order already completed"));

                if (order.FinalAmount != request.TransferAmount)
                    return Result.Failure(new Error("422", "Order Amount invalid"));

                order.Status = Constant.OrderStatus.ORDER_COMPLETED;
                await hubContext.Clients.Group(tran.Id.ToString())
                    .SendAsync("ReceivePaymentStatus", true, cancellationToken);
                break;
            }
        }

        return Result.Success("Handler successfully triggered.");
    }
}