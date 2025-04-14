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
        switch (request.Type)
        {
            case 0:
            {
                #region Subscription

                var tran = await systemTransactionRepository.FindByIdAsync(request.Id, cancellationToken,
                    x => x.SubscriptionPackage);

                if (tran == null || tran.IsDeleted) return Result.Failure(new Error("404", "Transaction not found"));

                if (tran.Status != 0) return Result.Failure(new Error("400", "Transaction already handler"));

                if (tran.Amount != request.TransferAmount)
                    return Result.Failure(new Error("422", "Transaction Amount invalid"));
                if (request.TransferAmount != tran.SubscriptionPackage.Price)
                {
                    await hubContext.Clients.Group(tran.Id.ToString())
                        .SendAsync("SubscriptionPriceChanged", false, cancellationToken);
                    break;
                }


                if (tran.TransactionDate > DateTimeOffset.Now)
                    return Result.Failure(new Error("400", "Transaction Date invalid"));

                // Update transaction status to 1 (completed)
                // The background job will look for transactions with status 1 and SubscriptionPackageId not null
                // to send confirmation emails
                tran.Status = 1;


                await hubContext.Clients.Group(tran.Id.ToString())
                    .SendAsync("ReceivePaymentStatus", true, cancellationToken);
                break;

                #endregion
            }
            case 1:
            {
                #region Clinic

                var tran = await clinicTransactionRepository.FindByIdAsync(request.Id, cancellationToken);

                if (tran == null || tran.IsDeleted) return Result.Failure(new Error("404", "Transaction not found"));

                if (tran.Status != Constant.OrderStatus.ORDER_PENDING)
                    return Result.Failure(new Error("400", "Transaction already handler"));

                if (tran.Amount != request.TransferAmount)
                    return Result.Failure(new Error("422", "Transaction Amount invalid"));

                if (tran.TransactionDate > DateTimeOffset.Now)
                    return Result.Failure(new Error("400", "Transaction Date invalid"));

                tran.Status = Constant.OrderStatus.ORDER_COMPLETED;
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

                #endregion
            }
            case 2:
            {
                #region Wallet

                var tran = await walletTransactionRepository.FindByIdAsync(request.Id, cancellationToken, x => x.User);

                if (tran == null || tran.IsDeleted) return Result.Failure(new Error("404", "Transaction not found"));

                if (tran.Status != Constant.WalletConstants.TransactionStatus.PENDING)
                    return Result.Failure(new Error("400", "Transaction already handler"));

                if (tran.Amount != request.TransferAmount)
                    return Result.Failure(new Error("422", "Transaction Amount invalid"));

                if (tran.TransactionDate > DateTimeOffset.Now)
                    return Result.Failure(new Error("400", "Transaction Date invalid"));

                tran.Status = Constant.WalletConstants.TransactionStatus.COMPLETED;
                tran.User!.Balance += tran.Amount;
                await hubContext.Clients.Group(tran.Id.ToString())
                    .SendAsync("ReceivePaymentStatus", true, cancellationToken);
                break;

                #endregion
            }
            case 3:
            {
                #region WITHDRAWAL

                var tran = await walletTransactionRepository.FindByIdAsync(request.Id, cancellationToken);
                if (tran == null || tran.IsDeleted) return Result.Failure(new Error("404", "Transaction not found"));

                if (tran.Status != Constant.WalletConstants.TransactionStatus.PENDING)
                    return Result.Failure(new Error("400", "Transaction already handler"));

                if (tran.Amount != request.TransferAmount)
                    return Result.Failure(new Error("422", "Transaction Amount invalid"));

               
                tran.Status = Constant.WalletConstants.TransactionStatus.COMPLETED;
                tran.Clinic.Balance -= tran.Amount;
                await hubContext.Clients.Group(tran.Id.ToString())
                    .SendAsync("ReceivePaymentStatus", true, cancellationToken);
                break;

                #endregion
            }
        }

        return Result.Success("Handler successfully triggered.");
    }
}