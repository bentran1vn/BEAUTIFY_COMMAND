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
        customerSchedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
        customerSchedule.Customer!.Balance += customerSchedule.Order!.DepositAmount;
        customerSchedule.Customer.Balance -= Math.Min(customerSchedule.ProcedurePriceType!.Price,
            customerSchedule.Order.DepositAmount);
        if (customerSchedule.Procedure!.StepIndex == 1)
        {
            if (clinic != null)
            {
                clinic.Balance -= customerSchedule.Order.DepositAmount;
                clinic.Balance += Math.Min(customerSchedule.ProcedurePriceType!.Price,
                    customerSchedule.Order.DepositAmount);
            }

            refundAmount += customerSchedule.Order.DepositAmount;


            // Update the main schedule status
            customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id,
                Constant.WalletConstants.TransactionStatus.CANCELLED);
        }
        else
        {
            // For step 2 or higher, get all schedules related to this order, INCLUDING the current one
            var allFeaturesSchedule = await _repositoryBase.FindAll(x =>
                    x.OrderId == customerSchedule.OrderId && x.Status != Constant.OrderStatus.ORDER_COMPLETED &&
                    x.Id != customerSchedule.Id)
                .ToListAsync(cancellationToken);

            if (allFeaturesSchedule.Count != 0)
            {
                // First, update all related schedules without updating the DB yet
                foreach (var schedule in allFeaturesSchedule)
                {
                    if (schedule.Status == Constant.OrderStatus.ORDER_PENDING)
                    {
                        var workingScheduleToDelete =
                            await _workingScheduleRepository.FindSingleAsync(ws => ws.CustomerScheduleId == schedule.Id,
                                cancellationToken);
                        if (workingScheduleToDelete != null)
                        {
                            // Uncomment if you want to delete the working schedule
                            _workingScheduleRepository.Remove(workingScheduleToDelete);
                            workingScheduleToDelete.WorkingScheduleDelete(workingScheduleToDelete.Id);
                        }
                    }

                    schedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
                    refundAmount += schedule.ProcedurePriceType!.Price;
                    schedule.UpdateCustomerScheduleStatus(schedule.Id,
                        Constant.WalletConstants.TransactionStatus.CANCELLED);
                    _repositoryBase.Update(schedule);
                    // Here's the fix for the CustomerScheduleId issue
                }


                // Update the balance after all calculations
                customerSchedule.Customer!.Balance += refundAmount;
                if (clinic != null)
                {
                    clinic.Balance -= refundAmount;
                }

                // Only update the main schedule once
            }
            else
            {
                // If no schedules found, still update the main schedule
                customerSchedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
                customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id,
                    Constant.WalletConstants.TransactionStatus.CANCELLED);
            }
        }

        _repositoryBase.Update(customerSchedule);
        var wallet = new WalletTransaction
        {
            Amount = refundAmount,
            UserId = customerSchedule.CustomerId,
            TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT_REFUND,
            Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
            IsMakeBySystem = true,
            OrderId = customerSchedule.OrderId,
            Description =
                $"Refund for cancelled schedule with service {customerSchedule.Procedure.Service.Name} at {customerSchedule.Date}",
        };
        _walletTransactionRepository.Add(wallet);

        return Result.Success();
    }
}