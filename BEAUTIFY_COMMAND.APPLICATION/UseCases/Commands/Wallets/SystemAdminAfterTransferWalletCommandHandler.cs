namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
internal sealed class SystemAdminAfterTransferWalletCommandHandler(
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    IMediaService media)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.SystemAdminAfterTransferWalletCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.SystemAdminAfterTransferWalletCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await walletTransactionRepository.FindByIdAsync(
            request.TransactionId,
            cancellationToken,
            x => x.Clinic!);

        // Validate transaction exists
        if (transaction == null || transaction.IsDeleted)
            return Result.Failure(new Error("404", "Transaction not found"));

        // Validate transaction status
        if (transaction.Status != Constant.WalletConstants.TransactionStatus.WAITING_FOR_PAYMENT)
            return Result.Failure(new Error("400", "Transaction already handled"));


        var imageUrl = await media.UploadImageAsync(request.Image);
        transaction.ProofImageUrl = imageUrl;

        // Update transaction status to completed
        transaction.Status = Constant.WalletConstants.TransactionStatus.COMPLETED;
        return Result.Success();
    }
}