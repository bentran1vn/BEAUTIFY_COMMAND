namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
internal sealed class CustomerOrderPaymentCommandHandler(
    IRepositoryBase<Order, Guid> orderRepositoryBase,
    IRepositoryBase<ClinicTransaction, Guid> clinicTransactionRepositoryBase)
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

        var transactionDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        var id = Guid.NewGuid();
        clinicTransactionRepositoryBase.Add(new ClinicTransaction
        {
            Id = id,
            ClinicId = order.Service!.ClinicServices!.FirstOrDefault()!.ClinicId,
            OrderId = order.Id,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Amount = order.FinalAmount!.Value,
            TransactionDate = transactionDate,
            PaymentMethod = request.PaymentMethod
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