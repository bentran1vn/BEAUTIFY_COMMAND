using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Wallets;
public static class Commands
{
    public record CustomerTopUpWalletCommand(
        decimal Amount) : ICommand;

    public record SystemAdminAfterTransferWalletCommand(
        Guid TransactionId,
        IFormFile Image) : ICommand;

    public record CreateWithdrawalRequestCommand(
        decimal Amount,
        string Description,
        Guid? ClinicId = null,
        string? BankAccount = null,
        string? BankName = null) : ICommand;

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

    public record ProcessServiceBookingDepositCommand(
        Guid CustomerId,
        Guid ServiceId,
        Guid OrderId,
        decimal DepositAmount,
        string Description) : ICommand;

    public record RefundServiceBookingDepositCommand(
        Guid CustomerId,
        Guid OrderId,
        Guid WalletTransactionId,
        string Description) : ICommand;
}