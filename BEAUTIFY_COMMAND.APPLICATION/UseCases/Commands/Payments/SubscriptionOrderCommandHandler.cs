namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
public class
    SubscriptionOrderCommandHandler(
        IRepositoryBase<Clinic, Guid> clinicRepository,
        IRepositoryBase<SubscriptionPackage, Guid> subscriptionPackageRepository,
        IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository)
    : ICommandHandler<CONTRACT.Services.Payments.Commands.SubscriptionOrderCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.SubscriptionOrderCommand request,
        CancellationToken cancellationToken)
    {
        var clinic = await clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken);

        if (clinic == null) return Result.Failure(new Error("404", "Clinic not found"));

        if (clinic.IsDeleted || !clinic.IsActivated || clinic.Status != 1)
            return Result.Failure(new Error("404", "Clinic is not activated"));

        var sub = await subscriptionPackageRepository.FindByIdAsync(request.SubscriptionId, cancellationToken);

        if (sub == null || sub.IsDeleted) return Result.Failure(new Error("404", "Subscription package not found"));
        if (sub.Price != request.CurrentAmount)
            return Result.Failure(new Error("404", "Giá gói đăng ký đã thay đổi"));

        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        var trans = new SystemTransaction
        {
            Id = Guid.NewGuid(),
            ClinicId = clinic.Id,
            SubscriptionPackageId = sub.Id,
            Status = 0,
            Amount = sub.Price,
            TransactionDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone),
            PaymentMethod = "SePay"
        };

        systemTransactionRepository.Add(trans);

        var qrUrl =
            $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={(int)sub.Price}&des=BeautifySub{trans.Id}";

        var result = new
        {
            TransactionId = trans.Id,
            BankNumber = "100879223979",
            BankGateway = "VietinBank",
            trans.Amount,
            OrderDescription = $"Beautify-{trans.Id}",
            QrUrl = qrUrl
        };

        return Result.Success(result);
    }
}