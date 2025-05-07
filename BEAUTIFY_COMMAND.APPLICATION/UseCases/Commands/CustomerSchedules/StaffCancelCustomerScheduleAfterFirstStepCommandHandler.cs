using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
public sealed class StaffCancelCustomerScheduleAfterFirstStepCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> _repositoryBase,
    IRepositoryBase<WalletTransaction, Guid> _walletTransactionRepository,
    IRepositoryBase<WorkingSchedule, Guid> _workingScheduleRepository,
    ICurrentUserService currentUserService,
    IRepositoryBase<Order, Guid> _orderRepository,
    IRepositoryBase<Clinic, Guid> _clinicRepository)
    : ICommandHandler<Command.StaffCancelCustomerScheduleAfterFirstStepCommand>
{
    public async Task<Result> Handle(Command.StaffCancelCustomerScheduleAfterFirstStepCommand request,
        CancellationToken cancellationToken)
    {
        // Optimize by executing both queries in parallel
        var orderTask = await _orderRepository.FindSingleAsync(x => x.Id == request.OrderId, cancellationToken);
        var customerScheduleTask = await _repositoryBase.FindAll(x => x.Id == request.CustomerScheduleId)
            .AsTracking()
            .FirstOrDefaultAsync(cancellationToken);
        var clinicTask = await _clinicRepository.FindSingleAsync(x => x.Id == currentUserService.ClinicId, cancellationToken);
        
        // Wait for all the necessary data


        var customerSchedule =  customerScheduleTask;

        if (customerSchedule == null)
            return Result.Failure(new Error("400", "Customer schedule not found"));
            
        if (customerSchedule.Status == Constant.WalletConstants.TransactionStatus.COMPLETED)
            return Result.Failure(new Error("400", "Customer schedule already completed"));

        var isRefundable = orderTask.Service.IsRefundable;
        decimal refundAmount = 0;
            
        // Update the schedule status
        customerSchedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
        customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id,
            Constant.WalletConstants.TransactionStatus.CANCELLED);
            
        // Handle working schedule cancellation
        await CancelWorkingSchedule(customerSchedule.Id);
        
        // Process based on step index
        if (customerSchedule.Procedure!.StepIndex == 1)
        {
            // First step calculation
            refundAmount = CalculateFirstStepRefund(customerSchedule);
            
            // Only update balances if refundable and there's an amount to refund
        }
        else
        {
            // For step 2 or higher, get all schedules related to this order
            var allFeaturesSchedule = await _repositoryBase.FindAll(x =>
                    x.OrderId == customerSchedule.OrderId &&
                    x.Status != Constant.OrderStatus.ORDER_COMPLETED &&
                    x.Id != customerSchedule.Id)
                .AsTracking()
                .ToListAsync(cancellationToken);

            // First add current schedule's price to refund amount
            refundAmount += customerSchedule.ProcedurePriceType!.Price;

            // Process all related schedules
            if (allFeaturesSchedule.Count > 0)
            {
                var workingScheduleTasks = new List<Task>();
                
                foreach (var schedule in allFeaturesSchedule)
                {
                    if (schedule.Status == Constant.OrderStatus.ORDER_PENDING)
                    {
                        // Collect working schedule cancellation tasks
                        workingScheduleTasks.Add(CancelWorkingSchedule(schedule.Id));
                    }

                    schedule.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
                    refundAmount += schedule.ProcedurePriceType!.Price;
                    schedule.UpdateCustomerScheduleStatus(schedule.Id,
                        Constant.WalletConstants.TransactionStatus.CANCELLED);
                    _repositoryBase.Update(schedule);
                }
                
                // Wait for all working schedule cancellations to complete
                if (workingScheduleTasks.Count > 0)
                {
                    await Task.WhenAll(workingScheduleTasks);
                }
            }

            // Only update balances if refundable and there's an amount to refund
        }

        if (isRefundable && refundAmount > 0)
        {
            customerSchedule.Customer!.Balance += refundAmount;
            if (clinicTask != null)
            {
                clinicTask.Balance -= refundAmount;
            }
        }

        // Create wallet transaction record if there's a refund amount
        if (refundAmount > 0)
        {
            var wallet = new WalletTransaction
            {
                Amount = refundAmount,
                UserId = customerSchedule.CustomerId,
                TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT_REFUND,
                Status = isRefundable 
                    ? Constant.WalletConstants.TransactionStatus.COMPLETED 
                    : Constant.WalletConstants.TransactionStatus.CANCELLED,
                IsMakeBySystem = true,
                OrderId = customerSchedule.OrderId,
                Description = $"Refund for cancelled schedule with service {customerSchedule.Procedure.Service.Name} at {customerSchedule.Date}",
            };
            _walletTransactionRepository.Add(wallet);
        }
        
        // Update current schedule
        _repositoryBase.Update(customerSchedule);
        
        return Result.Success();
    }
    
    private async Task CancelWorkingSchedule(Guid scheduleId)
    {
        var workingSchedule = await _workingScheduleRepository
            .FindAll(x => x.CustomerScheduleId == scheduleId)
            .AsTracking()
            .FirstOrDefaultAsync();
            
        if (workingSchedule != null)
        {
            _workingScheduleRepository.Remove(workingSchedule);
            workingSchedule.UpdateDoctorScheduleStatus([workingSchedule.Id],
                Constant.WalletConstants.TransactionStatus.CANCELLED);
        }
    }
    
    private static decimal CalculateFirstStepRefund(CustomerSchedule schedule)
    {
        // First step: Only refund the difference if deposit > price
        if (schedule.Order!.DepositAmount > schedule.ProcedurePriceType!.Price)
        {
            return schedule.Order.DepositAmount - schedule.ProcedurePriceType.Price;
        }
        
        return 0; // If deposit <= price, no refund (clinic keeps the deposit)
    }
}