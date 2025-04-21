namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
internal sealed class CustomerOrderPaymentCommandHandler(
    IRepositoryBase<Order, Guid> orderRepositoryBase,
    IRepositoryBase<ClinicTransaction, Guid> clinicTransactionRepositoryBase,
    IRepositoryBase<User, Guid> userRepositoryBase,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepositoryBase,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Payments.Commands.CustomerOrderPaymentCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.CustomerOrderPaymentCommand request,
        CancellationToken cancellationToken)
    {
        // Validate order
        var order = await orderRepositoryBase.FindByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            return Result.Failure(new Error("404", "Order Not Found"));
        if (order.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Order already completed"));

        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var transactionDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, vietnamTimeZone);
        var finalAmount = order.FinalAmount!.Value;
        var remainingAmount = finalAmount;
        var clinicId = order.Service!.ClinicServices!.FirstOrDefault()!.ClinicId;

        // Deduct from wallet balance if requested
        if (request.IsDeductFromCustomerBalance)
        {
            var userId = currentUserService.UserId;
            var user = await userRepositoryBase.FindByIdAsync(userId!.Value, cancellationToken);
            if (user == null)
                return Result.Failure(new Error("404", "User Not Found"));

            // Calculate amount to deduct (up to available balance)
            var deductAmount = Math.Min(user.Balance, finalAmount);

            if (deductAmount > 0)
            {
                // Deduct from user balance
                user.Balance -= deductAmount;
                userRepositoryBase.Update(user);

                // Create wallet transaction for the deduction
                var walletTransaction = new WalletTransaction
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Amount = deductAmount,
                    TransactionType = Constant.WalletConstants.TransactionType.SERVICE_DEPOSIT,
                    Status = Constant.WalletConstants.TransactionStatus.COMPLETED,
                    TransactionDate = transactionDate,
                    Description = $"Payment for order {order.Id}",
                    OrderId = order.Id
                };
                walletTransactionRepositoryBase.Add(walletTransaction);

                // Update remaining amount
                remainingAmount = finalAmount - deductAmount;
            }
        }

        // If balance fully covered the payment
        if (remainingAmount <= 0)
        {
            // Create completed clinic transaction
            clinicTransactionRepositoryBase.Add(new ClinicTransaction
            {
                Id = Guid.NewGuid(),
                ClinicId = clinicId,
                OrderId = order.Id,
                Status = Constant.OrderStatus.ORDER_COMPLETED,
                Amount = finalAmount,
                TransactionDate = transactionDate,
                PaymentMethod = request.PaymentMethod
            });

            return Result.Success(new { FullyPaid = true });
        }

        // Create pending clinic transaction for remaining amount
        var id = Guid.NewGuid();
        clinicTransactionRepositoryBase.Add(new ClinicTransaction
        {
            Id = id,
            ClinicId = clinicId,
            OrderId = order.Id,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Amount = remainingAmount,
            TransactionDate = transactionDate,
            PaymentMethod = request.PaymentMethod
        });

        // Generate payment link for remaining amount
        var qrUrl =
            $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={(int)remainingAmount}&des=BeautifyOrder{id}";
        var result = new
        {
            TransactionId = id,
            BankNumber = "100879223979",
            BankGateway = "VietinBank",
            Amount = remainingAmount,
            OriginalAmount = finalAmount,
            AmountPaidFromWallet = finalAmount - remainingAmount,
            OrderDescription = $"Customer Order: {request.OrderId}",
            QrUrl = qrUrl
        };

        return Result.Success(result);
    }
}