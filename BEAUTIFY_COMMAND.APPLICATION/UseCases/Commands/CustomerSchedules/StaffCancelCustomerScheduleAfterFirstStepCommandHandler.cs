using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class StaffCancelCustomerScheduleAfterFirstStepCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> _repositoryBase,
    IRepositoryBase<WalletTransaction, Guid> _walletTransactionRepository)
    : ICommandHandler<Command.StaffCancelCustomerScheduleAfterFirstStepCommand>
{
    public async Task<Result> Handle(Command.StaffCancelCustomerScheduleAfterFirstStepCommand request,
        CancellationToken cancellationToken)
    {
        var customerSchedule =
            await _repositoryBase.FindSingleAsync(x => x.Id == request.CustomerScheduleId, cancellationToken);
        if (customerSchedule == null)
            return Result.Failure(new Error("400", "Customer schedule not found"));
        if (customerSchedule.Status == Constant.WalletConstants.TransactionStatus.COMPLETED)
            return Result.Failure(new Error("400", "Customer schedule already completed"));
        if (customerSchedule.Procedure!.StepIndex != 1)
            return Result.Failure(new Error("400", "Customer schedule not in first step"));
        customerSchedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
        customerSchedule.Customer!.Balance += customerSchedule.Order!.DepositAmount;
        customerSchedule.Customer.Balance -= Math.Min(customerSchedule.ProcedurePriceType!.Price,
            customerSchedule.Order.DepositAmount);
        var wallet = new WalletTransaction
        {
            Amount = customerSchedule.Order.DepositAmount,
            UserId = customerSchedule.CustomerId,
            TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT_REFUND,
            Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
            IsMakeBySystem = true,
            OrderId = customerSchedule.OrderId
        };
        _walletTransactionRepository.Add(wallet);
        customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id,
            Constant.WalletConstants.TransactionStatus.CANCELLED);
        _repositoryBase.Update(customerSchedule);
        return Result.Success();
    }
}