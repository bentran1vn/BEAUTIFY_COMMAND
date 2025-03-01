using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Payments;

public static class Commands
{
    public class SepayBodyHook
    {
        public int id { get; set; }
        public string gateway { get; set; }
        public string transactionDate { get; set; }
        public string accountNumber { get; set; }
        public string code { get; set; }
        public string content { get; set; }
        public string transferType { get; set; }
        public int transferAmount { get; set; }
        public int accumulated { get; set; }
        public string subAccount { get; set; }
        public string referenceCode { get; set; }
        public string description { get; set; }
    }

    public class TriggerFromHookCommand: ICommand
    {
        public Guid Id{ get; set; }
        public string PaymentDate { get; set; }
        public int TransferAmount{ get; set; }
        public int Type{ get; set; }
    }

    public record SubscriptionOrderCommand(Guid SubscriptionId, Guid ClinicId) : ICommand;
    public record SubscriptionOrderBody(Guid SubscriptionId);
}