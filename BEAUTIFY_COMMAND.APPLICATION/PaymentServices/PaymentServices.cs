using Net.payOS;
using Net.payOS.Types;

namespace BEAUTIFY_COMMAND.APPLICATION.PaymentServices;

public class PaymentServices : IPaymentService
{
    private readonly IRepositoryBase<GlobalOrder, int> _repositoryBase;
    private readonly string _buyPackage = "https://beautify.asia/en/clinicManager/buy-package";
    private readonly string _userProfile = "https://beautify.asia/en/profile";
    private readonly string _cusSche = "https://beautify.asia/en/clinicStaff/customer-schedule";
    
    public PaymentServices(IRepositoryBase<GlobalOrder, int> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<string> CreatePaymentLink(Guid transactionId, double amount, string type)
    {
        PayOS payOS = new PayOS(
            "48df97f0-d4d8-4d46-ad4c-e892ff72d6f2",
            "ace73bb7-ce86-47ea-98c5-59fb86259ae5",
            "9760d8c0c56f92ac34b981d5e824866cfc821d0a3f66e64d8d2d8d8a88dedb24"
        );
        
        List<ItemData> items = new List<ItemData>();
        
        ItemData item = new ItemData(string.Empty, 1, 1);
        items.Add(item);

        int newOrderId = await _repositoryBase.FindAll(x => true).MaxAsync(x => x.Id) + 1;

        string returnUrl = "";
        switch (type)
        {
            case "SUB":
                //MuaSub
                returnUrl = $"{_buyPackage}?transactionId={transactionId}&type={type}";
                break;
            case "WALLET":
                //CusNap
                returnUrl = $"{_userProfile}?transactionId={transactionId}&type={type}";
                break;
            case "OVER":
                //OverSub
                returnUrl = $"{_buyPackage}?transactionId={transactionId}&type={type}";
                break;
            case "ORDER":
                //CusNap
                returnUrl = $"{_cusSche}?transactionId={transactionId}&type={type}";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        var paymentData = new PaymentData(
            orderCode: newOrderId,
            amount: (int)amount,
            description: "",
            items: items,
            returnUrl: returnUrl,
            cancelUrl: returnUrl
        );
        
        var paymentResult = await payOS.createPaymentLink(paymentData);
        
        _repositoryBase.Add(new GlobalOrder());
        
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