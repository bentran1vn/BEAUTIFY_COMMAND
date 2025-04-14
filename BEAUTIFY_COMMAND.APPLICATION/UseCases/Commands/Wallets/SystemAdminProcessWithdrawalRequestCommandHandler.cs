using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
internal sealed class SystemAdminProcessWithdrawalRequestCommandHandler(
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    IRepositoryBase<Clinic, Guid> clinicRepository)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.SystemAdminProcessWithdrawalRequestCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.Wallets.Commands.SystemAdminProcessWithdrawalRequestCommand request,
        CancellationToken cancellationToken)
    {
        var walletTransaction =
            await walletTransactionRepository.FindByIdAsync(request.WalletTransactionId, cancellationToken);
        if (walletTransaction == null)
            return Result.Failure(new Error("404", ErrorMessages.Wallet.WalletNotFound));

        var childClinic = await clinicRepository.FindByIdAsync(walletTransaction.ClinicId.Value, cancellationToken);
        if (childClinic == null)
        {
            return Result.Failure(new Error("404", ErrorMessages.Clinic.ClinicNotFound));
        }

        // Verify the wallet transaction is pending
        if (walletTransaction.Status != Constant.WalletConstants.TransactionStatus.WAITING_APPROVAL)
        {
            return Result.Failure(new Error("400", ErrorMessages.Wallet.InvalidTransactionStatus));
        }

        // Process the request based on approval status
        if (request.IsApproved)
        {
            // Update the withdrawal request
            if (childClinic.Balance < walletTransaction.Amount)
            {
                return Result.Failure(new Error("400", ErrorMessages.Clinic.InsufficientFunds));
            }

            var VietNamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            walletTransaction.TransactionDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, VietNamTimeZone);


            var qrUrl =
                $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={(int)walletTransaction.Amount}&des=Beautifywithdrawal{walletTransaction.Id}";

            var result = new
            {
                TransactionId = walletTransaction.Id,
                BankNumber = "100879223979",
                BankGateway = "VietinBank",
                walletTransaction.Amount,
                OrderDescription = $"BeautifyWITHDRAWAL-{walletTransaction.Id}",
                QrUrl = qrUrl
            };
            return Result.Success(result);
        }

        // Reject the withdrawal request
        walletTransaction.Status = Constant.WalletConstants.TransactionStatus.REJECTED_BY_SYSTEM;
        walletTransactionRepository.Update(walletTransaction);
        return Result.Success();
    }
}