using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class StaffCancelCustomerScheduleAfterFirstStepCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> _repositoryBase,
    IRepositoryBase<WalletTransaction, Guid> _walletTransactionRepository,
    IRepositoryBase<WorkingSchedule, Guid> _workingScheduleRepository,
    ICurrentUserService currentUserService,
    IRepositoryBase<Clinic, Guid> _clinicRepository)
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
        decimal refundAmount = 0;
        var clinic =
            await _clinicRepository.FindSingleAsync(x => x.Id == currentUserService.ClinicId,
                cancellationToken);
        if (customerSchedule.Procedure!.StepIndex == 1)
        {
            customerSchedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
            customerSchedule.Customer!.Balance += customerSchedule.Order!.DepositAmount;
            customerSchedule.Customer.Balance -= Math.Min(customerSchedule.ProcedurePriceType!.Price,
                customerSchedule.Order.DepositAmount);

            if (clinic != null)
            {
                clinic.Balance -= customerSchedule.Order.DepositAmount;
                clinic.Balance += Math.Min(customerSchedule.ProcedurePriceType!.Price,
                    customerSchedule.Order.DepositAmount);
            }

            var wallet = new WalletTransaction
            {
                Amount = customerSchedule.Order.DepositAmount,
                UserId = customerSchedule.CustomerId,
                TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT_REFUND,
                Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
                IsMakeBySystem = true,
                OrderId = customerSchedule.OrderId,
                Description =
                    $"Refund for cancelled schedule with service {customerSchedule.Procedure.Service.Name} at {customerSchedule.Date}",
            };
            _walletTransactionRepository.Add(wallet);
        }


        else
        {
            var allFeaturesSchedule = await _repositoryBase.FindAll(x =>
                    x.OrderId == customerSchedule.OrderId && x.Status != Constant.OrderStatus.ORDER_COMPLETED &&
                    x.Id != customerSchedule.Id)
                .ToListAsync(cancellationToken);
            if (allFeaturesSchedule.Count != 0)
            {
                foreach (var x in allFeaturesSchedule)
                {
                    x.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
                    refundAmount += x.ProcedurePriceType!.Price;
                    x.UpdateCustomerScheduleStatus(x.Id,
                        Constant.WalletConstants.TransactionStatus.CANCELLED);
                    var workingScheduleToDelete =
                        await _workingScheduleRepository.FindSingleAsync(x => x.CustomerScheduleId == x.Id,
                            cancellationToken);
                    _workingScheduleRepository.Remove(workingScheduleToDelete);
                    workingScheduleToDelete.WorkingScheduleDelete(workingScheduleToDelete.Id);
                }

                customerSchedule.Customer!.Balance += refundAmount;
                clinic.Balance -= refundAmount;
            }
        }


        var workingSchedule =
            await _workingScheduleRepository.FindSingleAsync(x => x.CustomerScheduleId == customerSchedule.Id,
                cancellationToken);
        if (workingSchedule != null)
        {
            _workingScheduleRepository.Remove(workingSchedule);
            workingSchedule.WorkingScheduleDelete(workingSchedule.Id);
        }

        customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id,
            Constant.WalletConstants.TransactionStatus.CANCELLED);
        _repositoryBase.Update(customerSchedule);
        return Result.Success();
    }
}