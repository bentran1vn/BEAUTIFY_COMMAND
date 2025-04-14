namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
internal sealed class ProcessWithdrawalRequestCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.ProcessWithdrawalRequestCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.ProcessWithdrawalRequestCommand request,
        CancellationToken cancellationToken)
    {
        var walletTransaction =
            await walletTransactionRepository.FindByIdAsync(request.WalletTransactionId, cancellationToken);


        var childClinic = await clinicRepository.FindByIdAsync(walletTransaction.ClinicId.Value, cancellationToken);
        if (childClinic == null)
        {
            return Result.Failure(new Error("404", "Child clinic not found"));
        }

        var parentClinic = await clinicRepository.FindByIdAsync(childClinic.ParentId.Value, cancellationToken);
        if (parentClinic == null)
        {
            return Result.Failure(new Error("404", "Parent clinic not found"));
        }


        // Process the request based on approval status
        if (request.IsApproved)
        {
            // Verify the child clinic still has sufficient balance
            if (childClinic.Balance < walletTransaction.Amount)
            {
                return Result.Failure(new Error("400", "Child clinic has insufficient funds"));
            }

            // Update the withdrawal request

            var VietNamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            walletTransaction.TransactionDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, VietNamTimeZone);


            var qrUrl =
                $"https://qr.sepay.vn/img?bank=MBBank&acc=0901928382&template=&amount={(int)walletTransaction.Amount}&des=BeautifyWITHDRAWAL{walletTransaction.Id}";

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

        // Update the withdrawal request as rejected
        walletTransaction.Status = Constant.WalletConstants.TransactionStatus.CANCELLED;
        walletTransactionRepository.Update(walletTransaction);


        return Result.Success();
    }
}