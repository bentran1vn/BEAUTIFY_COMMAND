using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.CustomerSchedules;
internal sealed class StaffUpdateCustomerScheduleStatusAfterCheckInCommandHandler(
    IRepositoryBase<CustomerSchedule, Guid> customerScheduleRepositoryBase,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepositoryBase,
    IRepositoryBase<User, Guid> userRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<Command.StaffUpdateCustomerScheduleStatusAfterCheckInCommand>
{
    public async Task<Result> Handle(Command.StaffUpdateCustomerScheduleStatusAfterCheckInCommand request,
        CancellationToken cancellationToken)
    {
        //take the vn time zone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var checkInDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);
        var customerSchedule = await customerScheduleRepositoryBase.FindByIdAsync(request.CustomerScheduleId,
            cancellationToken);
        if (customerSchedule!.Doctor!.ClinicId != currentUserService.ClinicId)
            return Result.Failure(new Error("403", "User is not a staff of this clinic"));
        if (customerSchedule == null)
            return Result.Failure(new Error("404", "Customer Schedule Not Found"));
        if (customerSchedule.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Customer Schedule already completed"));
        if (customerSchedule.Date != DateOnly.FromDateTime(checkInDate.Date))
            return Result.Failure(new Error("400", "Customer Schedule date is not today"));
        // Update the customer schedule status
        customerSchedule.Status = request.Status;
        customerScheduleRepositoryBase.Update(customerSchedule);
        customerSchedule.UpdateCustomerScheduleStatus(customerSchedule.Id, request.Status);

        // If this is the first meeting (status changed to IN_PROGRESS), refund the deposit
        if (request.Status != Constant.OrderStatus.ORDER_IN_PROGRESS) return Result.Success();
        // Find the deposit transaction for this order
        var depositTransaction = await walletTransactionRepositoryBase.FindSingleAsync(
            x => x.OrderId == customerSchedule.OrderId &&
                 x.TransactionType == Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT &&
                 x.Status == Constant.WalletConstants.TransactionStatus.COMPLETED,
            cancellationToken);

        if (depositTransaction == null) return Result.Success();
        try
        {
            // Find the customer to refund
            var user = await userRepository.FindByIdAsync(customerSchedule.CustomerId, cancellationToken);
            if (user != null)
            {
                // Create refund transaction
                var refundTransaction = CreateRefundTransaction(
                    customerSchedule.CustomerId,
                    customerSchedule.OrderId!.Value,
                    depositTransaction.Amount,
                    $"Refund deposit for booking {customerSchedule.OrderId} after first meeting",
                    depositTransaction.Id
                );

                // Add the deposit amount back to the user's balance
                user.Balance += depositTransaction.Amount;
                userRepository.Update(user);

                // Save the refund transaction
                walletTransactionRepositoryBase.Add(refundTransaction);
            }
        }
        catch (Exception ex)
        {
            // Log the error but don't fail the status update
            // The refund can be processed manually if needed
            Console.WriteLine($"Failed to process deposit refund: {ex.Message}");
        }

        return Result.Success();
    }

    /// <summary>
    /// Creates a new wallet transaction for the service booking deposit refund
    /// </summary>
    private static WalletTransaction CreateRefundTransaction(
        Guid userId,
        Guid orderId,
        decimal amount,
        string description,
        Guid originalTransactionId)
    {
        // Get current time in Vietnam timezone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, vietnamTimeZone);

        return new WalletTransaction
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = amount,
            TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT_REFUND,
            Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
            IsMakeBySystem = true,
            Description = description,
            TransactionDate = currentDateTime,
            CreatedOnUtc = DateTimeOffset.UtcNow,
            OrderId = orderId,
            RelatedTransactionId = originalTransactionId
        };
    }
}