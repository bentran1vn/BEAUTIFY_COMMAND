namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
internal sealed class CustomerOrderPaymentCommandHandler(
    IRepositoryBase<Order, Guid> orderRepositoryBase,
    IRepositoryBase<SystemTransaction, Guid> systemTransactionRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Payments.Commands.CustomerOrderPaymentCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.CustomerOrderPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepositoryBase.FindByIdAsync(request.OrderId, cancellationToken);
        if (order == null)
            return Result.Failure(new Error("404", "Order Not Found"));
        if (order.Status == Constant.OrderStatus.ORDER_COMPLETED)
            return Result.Failure(new Error("400", "Order already completed"));

        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var transactionDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);
        var id = Guid.NewGuid();
        systemTransactionRepositoryBase.Add(new SystemTransaction
        {
            Id = id,
            ClinicId = order.Service.ClinicServices.FirstOrDefault().ClinicId,
            OrderId = order.Id,
            SubscriptionPackageId = null,
            Status = 0,
            Amount = order.FinalAmount.Value,
            TransactionDate = transactionDate,
            PaymentMethod = request.PaymentMethod,
        });
        var qrUrl =
            $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={order.FinalAmount.Value}&des=BeautifyOrder{id}";
        var result = new
        {
            TransactionId = id,
            BankNumber = "100879223979",
            BankGateway = "VietinBank",
            order.FinalAmount.Value,
            OrderDescription = $"Customer Order: {request.OrderId}",
            QrUrl = qrUrl
        };
        return Result.Success(result);
    }
}