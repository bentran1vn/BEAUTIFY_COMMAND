using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;

/// <summary>
/// Handler for processing withdrawal requests by clinic managers
/// </summary>
internal sealed class ProcessWithdrawalRequestCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.ProcessWithdrawalRequestCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.ProcessWithdrawalRequestCommand request,
        CancellationToken cancellationToken)
    {
        // Get and validate the transaction
        var walletTransaction = await walletTransactionRepository.FindByIdAsync(request.WalletTransactionId, cancellationToken);
        if (walletTransaction == null)
        {
            return Result.Failure(new Error("404", ErrorMessages.Wallet.WalletNotFound));
        }

        // Validate transaction status
        if (walletTransaction.Status != Constant.WalletConstants.TransactionStatus.PENDING)
        {
            return Result.Failure(new Error("400", "This withdrawal request has already been processed"));
        }

        // Get and validate the clinic
        var childClinic = await GetAndValidateChildClinic(walletTransaction, cancellationToken);
        if (childClinic.IsFailure)
        {
            return childClinic;
        }

        // Process the request based on approval status
        return await ProcessWithdrawalRequest(request.IsApproved, walletTransaction, childClinic.Value, request.RejectionReason, cancellationToken);
    }

    /// <summary>
    /// Gets and validates the child clinic associated with the transaction
    /// </summary>
    private async Task<Result<Clinic>> GetAndValidateChildClinic(WalletTransaction transaction, CancellationToken cancellationToken)
    {
        if (!transaction.ClinicId.HasValue)
        {
            return Result.Failure<Clinic>(new Error("400", "Transaction is not associated with a clinic"));
        }

        var childClinic = await clinicRepository.FindByIdAsync(transaction.ClinicId.Value, cancellationToken);
        if (childClinic == null)
        {
            return Result.Failure<Clinic>(new Error("404", ErrorMessages.Clinic.ClinicNotFound));
        }

        return childClinic.ParentId == null ? Result.Failure<Clinic>(new Error("400", ErrorMessages.Clinic.ClinicIsNotABranch)) : Result.Success(childClinic);
    }

    /// <summary>
    /// Processes the withdrawal request based on approval status
    /// </summary>
    private async Task<Result> ProcessWithdrawalRequest(
        bool isApproved, 
        WalletTransaction transaction, 
        Clinic childClinic,
        string? rejectionReason,
        CancellationToken cancellationToken)
    {
        if (isApproved)
        {
            // Verify the child clinic still has sufficient balance
            if (childClinic.Balance < transaction.Amount)
            {
                return Result.Failure(new Error("400", ErrorMessages.Clinic.InsufficientFunds));
            }

            // Update the transaction status
            transaction.Status = Constant.WalletConstants.TransactionStatus.WAITING_APPROVAL;
        }
        else
        {
            // Update the transaction as rejected
            transaction.Status = Constant.WalletConstants.TransactionStatus.REJECTED;
            transaction.Description = !string.IsNullOrEmpty(rejectionReason) 
                ? $"{transaction.Description} - Rejected: {rejectionReason}"
                : $"{transaction.Description} - Rejected";
        }
        

        walletTransactionRepository.Update(transaction);
        return Result.Success();
    }
}