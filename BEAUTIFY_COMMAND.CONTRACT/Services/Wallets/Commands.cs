namespace BEAUTIFY_COMMAND.CONTRACT.Services.Wallets;
public static class Commands
{
    public record CustomerTopUpWalletCommand(
        decimal Amount) : ICommand;

    public record CreateWithdrawalRequestCommand(
        decimal Amount,
        string Description,
        Guid? ClinicId = null) : ICommand;

    public record ProcessWithdrawalRequestCommand(
        Guid WalletTransactionId,
        bool IsApproved,
        string? RejectionReason = null) : ICommand;

    public record SystemAdminProcessWithdrawalRequestCommand(
        Guid WalletTransactionId,
        bool IsApproved,
        string? RejectionReason = null) : ICommand;

    public record WithdrawFromClinicWalletCommand(
        Guid ClinicId,
        decimal Amount,
        string Description) : ICommand;

    public record CustomerWithdrawFromWalletCommand(
        decimal Amount,
        string BankName,
        string BankAccountNumber,
        string AccountHolderName) : ICommand;
}