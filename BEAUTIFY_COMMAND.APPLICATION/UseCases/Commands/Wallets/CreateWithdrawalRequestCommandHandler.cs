using BEAUTIFY_COMMAND.DOMAIN;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Wallets;
internal sealed class CreateWithdrawalRequestCommandHandler(
    IRepositoryBase<Clinic, Guid> clinicRepository,
    IRepositoryBase<WalletTransaction, Guid> walletTransactionRepository,
    ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Wallets.Commands.CreateWithdrawalRequestCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Wallets.Commands.CreateWithdrawalRequestCommand request,
        CancellationToken cancellationToken)
    {
        // Get the child clinic
        var childClinic = await clinicRepository.FindByIdAsync(currentUserService.ClinicId!.Value, cancellationToken);
        if (childClinic == null)
        {
            return Result.Failure(new Error("404", ErrorMessages.Clinic.ClinicNotFound));
        }

        // Verify the child clinic has a parent
        if (childClinic.ParentId == null)
        {
            return Result.Failure(new Error("400", ErrorMessages.Clinic.ClinicIsNotABranch));
        }

        // Get the parent clinic
        var parentClinic = await clinicRepository.FindByIdAsync(childClinic.ParentId.Value, cancellationToken);
        if (parentClinic == null)
        {
            return Result.Failure(new Error("404", ErrorMessages.Clinic.ParentClinicNotFound));
        }

        // Verify the amount is valid
        if (request.Amount < 2000)
        {
            return Result.Failure(new Error("400", ErrorMessages.Clinic.AmountMustBeGreaterThan2000));
        }

        // Verify the child clinic has sufficient balance
        if (childClinic.Balance < request.Amount)
        {
            return Result.Failure(new Error("400", ErrorMessages.Clinic.InsufficientFunds));
        }

        var walletTransaction = new WalletTransaction
        {
            Id = Guid.NewGuid(),
            ClinicId = currentUserService.ClinicId,
            Amount = request.Amount,
            TransactionType = Constant.WalletConstants.TransactionType.WITHDRAWAL,
            Status = Constant.WalletConstants.TransactionStatus.PENDING,
            IsMakeBySystem = false,
            Description = request.Description
        };


        walletTransactionRepository.Add(walletTransaction);

        return Result.Success();
    }
}