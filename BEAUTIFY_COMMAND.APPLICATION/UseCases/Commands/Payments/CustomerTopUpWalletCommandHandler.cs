namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;
internal sealed class
    CustomerTopUpWalletCommandHandler(
        ICurrentUserService currentUserService,
        IRepositoryBase<User, Guid> userRepositoryBase,
        IRepositoryBase<WalletTransaction, Guid> walletTransactionRepositoryBase)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.CustomerTopUpWalletCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.CustomerTopUpWalletCommand request,
        CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        var user = await userRepositoryBase.FindByIdAsync(userId!.Value, cancellationToken);
        if (user == null)
            return Result.Failure(new Error("404", "User Not Found"));

        var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        var walletTransaction = new WalletTransaction
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Amount = request.Amount,
            TransactionType = Constant.WalletConstants.TransactionType.DEPOSIT,
            Status = Constant.WalletConstants.TransactionStatus.PENDING,
            TransactionDate = currentDateTime
        };
        

        walletTransactionRepositoryBase.Add(walletTransaction);
        var qrUrl =
            $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={(int)walletTransaction.Amount}&des=BeautifyWallet{walletTransaction.Id}";
        var result = new
        {
            TransactionId = walletTransaction.Id,
            BankNumber = "100879223979",
            BankGateway = "VietinBank",
            walletTransaction.Amount,
            OrderDescription = $"BeautifyWallet-{walletTransaction.Id}",
            QrUrl = qrUrl
        };
        return Result.Success(result);
    }
}