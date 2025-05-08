namespace BEAUTIFY_COMMAND.CONTRACT.Services.Payments;
public static class Commands
{
    public class SepayBodyHook
    {
        public Guid Id { get; set; }
        public int Types { get; set; }
    }

    public class TriggerFromHookCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Types { get; set; }
    }

    public record CustomerOrderPaymentCommand(Guid OrderId, string PaymentMethod, bool IsDeductFromCustomerBalance)
        : ICommand;

    public record SubscriptionOrderCommand(Guid SubscriptionId, Guid ClinicId, decimal CurrentAmount) : ICommand;

    public record SubscriptionOrderBody(Guid SubscriptionId, decimal CurrentAmount);

    public record SubscriptionOverOrderBody(
        Guid SubscriptionId,
        decimal CurrentAmount,
        int AdditionBranch,
        int AdditionLiveStream);

    public record SubscriptionOverOrderCommand(
        Guid SubscriptionId,
        Guid ClinicId,
        decimal CurrentAmount,
        int AdditionBranch,
        int AdditionLiveStream) : ICommand;
}