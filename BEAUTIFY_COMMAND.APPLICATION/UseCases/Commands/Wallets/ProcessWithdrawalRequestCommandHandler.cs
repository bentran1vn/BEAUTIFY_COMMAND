using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
internal sealed class ProcessWithdrawalRequestCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.ProcessWithdrawalRequestCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.ProcessWithdrawalRequestCommand request,
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

        var parentClinic = await clinicRepository.FindByIdAsync(childClinic.ParentId.Value, cancellationToken);
        if (parentClinic == null)
        {
            return Result.Failure(new Error("404", ErrorMessages.Clinic.ParentClinicNotFound));
        }


        // Process the request based on approval status
        if (request.IsApproved)
        {
            // Verify the child clinic still has sufficient balance
            if (childClinic.Balance < walletTransaction.Amount)
            {
                return Result.Failure(new Error("400", ErrorMessages.Clinic.InsufficientFunds));
            }

            // Update the withdrawal request

            /* */
            walletTransaction.Status = Constant.WalletConstants.TransactionStatus.WAITING_APPROVAL;
            walletTransactionRepository.Update(walletTransaction);
        }

        walletTransaction.Status = Constant.WalletConstants.TransactionStatus.REJECTED;
        walletTransactionRepository.Update(walletTransaction);


        return Result.Success();
    }
}