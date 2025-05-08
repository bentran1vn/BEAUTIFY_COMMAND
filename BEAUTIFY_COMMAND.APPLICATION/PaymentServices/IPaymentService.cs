namespace BEAUTIFY_COMMAND.APPLICATION.PaymentServices;

public interface IPaymentService
{
    Task<string> CreatePaymentLink(Guid transactionId, double amount, string type);

    Task<string> CreatePaymentLinkForSubscription(Guid transactionId);

    Task<string> GetPaymentLinkInformation();

    Task<bool> CancelPaymentLink(); 
}