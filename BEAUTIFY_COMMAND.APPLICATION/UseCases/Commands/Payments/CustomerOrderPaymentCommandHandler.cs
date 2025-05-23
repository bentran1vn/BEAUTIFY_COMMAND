﻿using BEAUTIFY_COMMAND.APPLICATION.PaymentServices;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
internal sealed class CustomerOrderPaymentCommandHandler(
    IRepositoryBase<Order, Guid> orderRepositoryBase,
    IRepositoryBase<ClinicTransaction, Guid> clinicTransactionRepositoryBase,
    IRepositoryBase<User, Guid> userRepositoryBase,
    IRepositoryBase<Clinic, Guid> clinicRepositoryBase,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepositoryBase,
    ICurrentUserService currentUserService,
    IPaymentService paymentService)
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

        var transactionDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        var finalAmount = order.FinalAmount!.Value;
        var remainingAmount = finalAmount;
        var clinicId = currentUserService.ClinicId;


        // Deduct from wallet balance if requested
        if (request.IsDeductFromCustomerBalance)
        {
            var userId = order.CustomerId;
            var user = await userRepositoryBase.FindByIdAsync(userId, cancellationToken);
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
            //  order.Service.ClinicServices.FirstOrDefault().Clinics.Balance += finalAmount;
            var clinic = await clinicRepositoryBase.FindByIdAsync(clinicId.Value, cancellationToken);
            if (clinic == null)
                return Result.Failure(new Error("404", "Clinic Not Found"));
            clinic.Balance += finalAmount;
            clinicRepositoryBase.Update(clinic);
            order.Status = Constant.OrderStatus.ORDER_COMPLETED;

            orderRepositoryBase.Update(order);
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
        var qrUrl = await paymentService.CreatePaymentLink(id, (double)remainingAmount, "ORDER");
        
        var result = new
        {
            TransactionId = id,
            BankNumber = "0901928382",
            BankGateway = "MBBank",
            Amount = remainingAmount,
            OriginalAmount = finalAmount,
            AmountPaidFromWallet = finalAmount - remainingAmount,
            OrderDescription = $"Customer Order: {request.OrderId}",
            QrUrl = qrUrl
        };

        return Result.Success(result);
    }
}