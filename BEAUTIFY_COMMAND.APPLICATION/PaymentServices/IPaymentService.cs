namespace BEAUTIFY_COMMAND.APPLICATION.PaymentServices;

public interface IPaymentService
{
    Task<string> CreatePaymentLink(long transactionLongId, Guid transactionId, double amount, int type);

    Task<string> CreatePaymentLinkForSubscription(Guid transactionId);

    Task<string> GetPaymentLinkInformation();

    Task<bool> CancelPaymentLink(); 
}