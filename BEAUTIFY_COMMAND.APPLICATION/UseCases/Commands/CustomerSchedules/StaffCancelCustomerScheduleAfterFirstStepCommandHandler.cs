using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
public sealed class StaffCancelCustomerScheduleAfterFirstStepCommandHandler(
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
        await _repositoryBase.FindAll(x => x.Id == request.CustomerScheduleId)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken);
    if (customerSchedule == null)
        return Result.Failure(new Error("400", "Customer schedule not found"));
    if (customerSchedule.Status == Constant.WalletConstants.TransactionStatus.COMPLETED)
        return Result.Failure(new Error("400", "Customer schedule already completed"));

    decimal refundAmount = 0;
    var clinic = await _clinicRepository.FindSingleAsync(x => x.Id == currentUserService.ClinicId,
        cancellationToken);
    customerSchedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;

    await GetWorkingSchedule(customerSchedule.Id);
    
    if (customerSchedule.Procedure!.StepIndex == 1)
    {
        // First step: Only refund the difference if deposit > price
        if (customerSchedule.Order!.DepositAmount > customerSchedule.ProcedurePriceType!.Price)
        {
            refundAmount = customerSchedule.Order.DepositAmount - customerSchedule.ProcedurePriceType.Price;
        }
        else
        {
            // If deposit <= price, no refund (clinic keeps the deposit)
            refundAmount = 0;
        }
        
        customerSchedule.Customer!.Balance += refundAmount;
        if (clinic != null)
        {
            clinic.Balance -= refundAmount;
        }

        // Update the main schedule status
        customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id,
            Constant.WalletConstants.TransactionStatus.CANCELLED);
    }
    else
    {
        // For step 2 or higher, get all schedules related to this order
        var allFeaturesSchedule = await _repositoryBase.FindAll(x =>
                    x.OrderId == customerSchedule.OrderId && 
                    x.Status != Constant.OrderStatus.ORDER_COMPLETED &&
                    x.Id != customerSchedule.Id)  // Keep excluding current schedule from query
                .AsTracking()
                .ToListAsync(cancellationToken);

        // First add current schedule's price to refund amount
        refundAmount += customerSchedule.ProcedurePriceType!.Price;
        
        if (allFeaturesSchedule.Count != 0)
        {
            // Update all related schedules
            foreach (var schedule in allFeaturesSchedule)
            {
                if (schedule.Status == Constant.OrderStatus.ORDER_PENDING)
                {
                    await GetWorkingSchedule(schedule.Id);
                }

                schedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
                refundAmount += schedule.ProcedurePriceType!.Price;
                schedule.UpdateCustomerScheduleStatus(schedule.Id,
                    Constant.WalletConstants.TransactionStatus.CANCELLED);
                _repositoryBase.Update(schedule);
            }
        }
        
        // Update the balance after all calculations
        customerSchedule.Customer!.Balance += refundAmount;
        if (clinic != null)
        {
            clinic.Balance -= refundAmount;
        }
    }
    
    // Update current schedule
    _repositoryBase.Update(customerSchedule);
    
    // Create wallet transaction for the refund
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

    private async Task GetWorkingSchedule(Guid Id)
    {
        var workingSchedule = await _workingScheduleRepository.FindAll(x => x.CustomerScheduleId == Id).AsTracking()
            .FirstOrDefaultAsync();
        if (workingSchedule != null)
        {
            _workingScheduleRepository.Remove(workingSchedule);
            workingSchedule.UpdateDoctorScheduleStatus([workingSchedule.Id],
                Constant.WalletConstants.TransactionStatus.CANCELLED);
        }
    }
}