using BEAUTIFY_COMMAND.APPLICATION.PaymentServices;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
public class SubscriptionOverOrderCommandHandler
    : ICommandHandler<CONTRACT.Services.Payments.Commands.SubscriptionOverOrderCommand>
{
    private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;
    private readonly IRepositoryBase<SubscriptionPackage, Guid> _subscriptionPackageRepository;
    private readonly IRepositoryBase<SystemTransaction, Guid> _systemTransactionRepository;
    private readonly IPaymentService _paymentService;

    public SubscriptionOverOrderCommandHandler(IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository,
        IRepositoryBase<SubscriptionPackage, Guid> subscriptionPackageRepository,
        IRepositoryBase<Clinic, Guid> clinicRepository, IPaymentService paymentService)
    {
        _systemTransactionRepository = systemTransactionRepository;
        _subscriptionPackageRepository = subscriptionPackageRepository;
        _clinicRepository = clinicRepository;
        _paymentService = paymentService;
    }

    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.SubscriptionOverOrderCommand request,
        CancellationToken cancellationToken)
    {
        var clinic = await _clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken);

        if (clinic == null) return Result.Failure(new Error("404", "Clinic not found"));

        if (clinic.IsDeleted || !clinic.IsActivated || clinic.Status != 1)
            return Result.Failure(new Error("404", "Clinic is not activated"));

        var sub = await _subscriptionPackageRepository.FindByIdAsync(request.SubscriptionId, cancellationToken);

        if (sub == null || sub.IsDeleted) return Result.Failure(new Error("404", "Subscription package not found"));

        if (sub.PriceMoreBranch * request.AdditionBranch + sub.PriceMoreLivestream * request.AdditionLiveStream !=
            request.CurrentAmount)
            return Result.Failure(new Error("404", "Giá gói đăng ký đã thay đổi"));

        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        var pricePayment = sub.PriceMoreBranch * request.AdditionBranch +
                           sub.PriceMoreLivestream * request.AdditionLiveStream;

        var trans = new SystemTransaction
        {
            Id = Guid.NewGuid(),
            ClinicId = clinic.Id,
            SubscriptionPackageId = sub.Id,
            Status = 0,
            Amount = pricePayment,
            AdditionLivestreams = request.AdditionLiveStream,
            AdditionBranches = request.AdditionBranch,
            TransactionDate = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone),
            PaymentMethod = "SePay"
        };

        _systemTransactionRepository.Add(trans);

        var qrUrl = await _paymentService.CreatePaymentLink(trans.Id, (double)trans.Amount, "BuyPackage");

        var result = new
        {
            TransactionId = trans.Id,
            BankNumber = "0901928382",
            BankGateway = "MBBank",
            trans.Amount,
            OrderDescription = $"Beautify-{trans.Id}",
            QrUrl = qrUrl
        };

        return Result.Success(result);
    }
}