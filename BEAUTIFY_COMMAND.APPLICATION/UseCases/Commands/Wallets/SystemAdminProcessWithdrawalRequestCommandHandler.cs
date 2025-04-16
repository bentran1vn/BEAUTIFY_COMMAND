using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
/// <summary>
/// Handler for system administrators to process withdrawal requests
/// </summary>
internal sealed class SystemAdminProcessWithdrawalRequestCommandHandler(
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    IRepositoryBase<Clinic, Guid> clinicRepository)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.SystemAdminProcessWithdrawalRequestCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.Wallets.Commands.SystemAdminProcessWithdrawalRequestCommand request,
        CancellationToken cancellationToken)
    {
        // Get and validate the transaction
        var (transaction, clinic) = await GetAndValidateTransaction(request.WalletTransactionId, cancellationToken);
        if (transaction == null || clinic == null)
        {
            return Result.Failure(new Error("404", ErrorMessages.Wallet.WalletNotFound));
        }

        // Verify the wallet transaction is in the correct state
        if (transaction.Status != Constant.WalletConstants.TransactionStatus.WAITING_APPROVAL)
        {
            return Result.Failure(new Error("400", ErrorMessages.Wallet.InvalidTransactionStatus));
        }

        // Process the request based on approval status
        return request.IsApproved
            ? await ApproveWithdrawal(transaction, clinic)
            : await RejectWithdrawal(transaction, clinic);
    }

    /// <summary>
    /// Gets and validates the transaction and associated clinic
    /// </summary>
    private async Task<(WalletTransaction? Transaction, Clinic? Clinic)> GetAndValidateTransaction(
        Guid transactionId,
        CancellationToken cancellationToken)
    {
        var transaction = await walletTransactionRepository.FindByIdAsync(transactionId, cancellationToken);
        if (transaction is not { ClinicId: not null })
        {
            return (null, null);
        }

        var clinic = await clinicRepository.FindByIdAsync(transaction.ClinicId.Value, cancellationToken);
        return clinic == null ? (transaction, null) : (transaction, clinic);
    }

    /// <summary>
    /// Approves the withdrawal request and generates payment information
    /// </summary>
    private async Task<Result> ApproveWithdrawal(
        WalletTransaction transaction,
        Clinic clinic)
    {
        // Verify sufficient funds
        if (clinic.Balance < transaction.Amount)
        {
            return Result.Failure(new Error("400", ErrorMessages.Clinic.InsufficientFunds));
        }

        // Update transaction with Vietnam timezone
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        transaction.TransactionDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, vietnamTimeZone);
        transaction.Status = Constant.WalletConstants.TransactionStatus.WAITING_FOR_PAYMENT;
        transaction.ModifiedOnUtc = DateTimeOffset.UtcNow;

        // Save changes


        // Generate payment information
        var qrUrl =
            $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={(int)transaction.Amount}&des=Beautifywithdrawal{transaction.Id}";
        var result = new
        {
            TransactionId = transaction.Id,
            BankNumber = "100879223979",
            BankGateway = "VietinBank",
            transaction.Amount,
            OrderDescription = $"BeautifyWITHDRAWAL-{transaction.Id}",
            QrUrl = qrUrl
        };
        transaction.NewestQrUrl = qrUrl;
        walletTransactionRepository.Update(transaction);
        return Result.Success(result);
    }

    /// <summary>
    /// Rejects the withdrawal request
    /// </summary>
    private async Task<Result> RejectWithdrawal(
        WalletTransaction transaction,
        Clinic clinic)
    {
        transaction.Status = Constant.WalletConstants.TransactionStatus.REJECTED_BY_SYSTEM;
        walletTransactionRepository.Update(transaction);

        // Add the amount back to the clinic's balance if rejected by system admin
        clinic.Balance += transaction.Amount;
        clinicRepository.Update(clinic);

        return Result.Success();
    }
}