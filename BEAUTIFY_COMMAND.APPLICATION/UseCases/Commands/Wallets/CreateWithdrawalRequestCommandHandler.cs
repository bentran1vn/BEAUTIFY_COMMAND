using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
/// <summary>
/// Handler for creating withdrawal requests from a child clinic to its parent
/// </summary>
internal sealed class CreateWithdrawalRequestCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.CreateWithdrawalRequestCommand>
{
    private const decimal MINIMUM_WITHDRAWAL_AMOUNT = 2000;

    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.CreateWithdrawalRequestCommand request,
        CancellationToken cancellationToken)
    {
        // Get clinic information
        var clinicId = request.ClinicId ?? currentUserService.ClinicId;
        var clinic = await clinicRepository.FindByIdAsync(clinicId.Value, cancellationToken);
        if (clinic is null)
            return Result.Failure(new Error("404", ErrorMessages.Clinic.ClinicNotFound));

        // Validate withdrawal conditions
        if (request.Amount < MINIMUM_WITHDRAWAL_AMOUNT)
            return Result.Failure(new Error("400", ErrorMessages.Clinic.AmountMustBeGreaterThan2000));

        if (clinic.Balance < request.Amount)
            return Result.Failure(new Error("400", ErrorMessages.Clinic.InsufficientFunds));

        // Determine transaction status based on request source
        var isSystemGenerated = request.ClinicId is null;
        var status = isSystemGenerated
            ? Constant.WalletConstants.TransactionStatus.PENDING
            : Constant.WalletConstants.TransactionStatus.WAITING_APPROVAL;


        var wallet = new WalletTransaction
        {
            Id = Guid.NewGuid(),
            ClinicId = clinicId,
            Amount = request.Amount,
            TransactionType = Constant.WalletConstants.TransactionType.WITHDRAWAL,
            Status = status,
            IsMakeBySystem = false,
            Description = request.Description,
            CreatedOnUtc = DateTimeOffset.UtcNow
        };
        var bankName = request.BankName ?? clinic.BankName;
        var bankAccount = request.BankAccount ?? clinic.BankAccountNumber;

        var qrUrl =
            $"https://qr.sepay.vn/img?bank={bankName}&acc={bankAccount}&template=&amount={(int)wallet.Amount}&des=Beautifywithdrawal{wallet.Id}";
        wallet.NewestQrUrl = qrUrl;
        // Record transaction
        walletTransactionRepository.Add(wallet);

        // Update clinic balance
        clinic.Balance -= request.Amount;
        clinicRepository.Update(clinic);

        return Result.Success();
    }
}