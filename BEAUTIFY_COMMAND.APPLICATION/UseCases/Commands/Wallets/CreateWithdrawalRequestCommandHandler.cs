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
        // Validate clinic exists and user has permission
        var clinicId = currentUserService.ClinicId;
        if (!clinicId.HasValue)
        {
            return Result.Failure(new Error("403",
                "You must be associated with a clinic to create a withdrawal request"));
        }

        // Get the child clinic with a single database call
        var childClinic = await clinicRepository.FindByIdAsync(clinicId.Value, cancellationToken);
        if (childClinic == null)
        {
            return Result.Failure(new Error("404", ErrorMessages.Clinic.ClinicNotFound));
        }

        // Validate request parameters
        var validationResult = ValidateWithdrawalRequest(childClinic, request.Amount);
        if (validationResult.IsFailure)
        {
            return validationResult;
        }

        // Create and save the transaction in a single operation
        var walletTransaction = CreateWalletTransaction(clinicId.Value, request.Amount, request.Description);
        walletTransactionRepository.Add(walletTransaction);

        return Result.Success();
    }

    /// <summary>
    /// Validates the withdrawal request parameters
    /// </summary>
    private static Result ValidateWithdrawalRequest(Clinic childClinic, decimal amount)
    {
        // Verify the child clinic has a parent
        if (childClinic.ParentId == null)
        {
            return Result.Failure(new Error("400", ErrorMessages.Clinic.ClinicIsNotABranch));
        }

        // Verify the amount is valid
        if (amount < MINIMUM_WITHDRAWAL_AMOUNT)
        {
            return Result.Failure(new Error("400", ErrorMessages.Clinic.AmountMustBeGreaterThan2000));
        }

        // Verify the child clinic has sufficient balance
        return childClinic.Balance < amount
            ? Result.Failure(new Error("400", ErrorMessages.Clinic.InsufficientFunds))
            : Result.Success();
    }

    /// <summary>
    /// Creates a new wallet transaction for the withdrawal request
    /// </summary>
    private static WalletTransaction CreateWalletTransaction(Guid clinicId, decimal amount, string description)
    {
        return new WalletTransaction
        {
            Id = Guid.NewGuid(),
            ClinicId = clinicId,
            Amount = amount,
            TransactionType = Constant.WalletConstants.TransactionType.WITHDRAWAL,
            Status = Constant.WalletConstants.TransactionStatus.PENDING,
            IsMakeBySystem = false,
            Description = description,
            CreatedOnUtc = DateTimeOffset.UtcNow
        };
    }
}