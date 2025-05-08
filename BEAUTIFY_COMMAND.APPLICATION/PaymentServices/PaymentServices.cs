using Net.payOS;
using Net.payOS.Types;

namespace BEAUTIFY_COMMAND.APPLICATION.PaymentServices;

public class PaymentServices : IPaymentService
{
    private readonly string _appBaseUrLDev = "https://localhost:5292/api/v1/payments";
    
    public async Task<string> CreatePaymentLink(long transactionLongId, Guid transactionId, double amount, int type)
    {
        PayOS payOS = new PayOS("48df97f0-d4d8-4d46-ad4c-e892ff72d6f2",
            "ace73bb7-ce86-47ea-98c5-59fb86259ae5",
            "9760d8c0c56f92ac34b981d5e824866cfc821d0a3f66e64d8d2d8d8a88dedb24");
        
        List<ItemData> items = new List<ItemData>();
        
        ItemData item = new ItemData( string.Empty, 1, 1);
        items.Add(item);

        string description = "";
        switch (type)
        {
            case 1:
                description = $"BeautifyOrder{transactionId}";
                break;
            case 2:
                description = "Payment for subscription";
                break;
            case 3:
                description = "Payment for order";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        var paymentData = new PaymentData(
            orderCode: 4,
            amount: (int)amount,
            description: "",
            items: items,
            returnUrl: _appBaseUrLDev + $"/payOs/success?transactionId={transactionId}",
            cancelUrl: _appBaseUrLDev + $"/payOs/error?transactionId={transactionId}"
        );
        
        var paymentResult = await payOS.createPaymentLink(paymentData);
        return paymentResult.checkoutUrl;
    }

    public Task<string> CreatePaymentLinkForSubscription(Guid transactionId)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetPaymentLinkInformation()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancelPaymentLink()
    {
        throw new NotImplementedException();
    }
}