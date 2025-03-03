using Microsoft.AspNetCore.SignalR;

namespace BEAUTIFY_COMMAND.APPLICATION.Hub;

public class PaymentHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task JoinPaymentSession(string transactionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, transactionId);
        await Clients.Caller.SendAsync("JoinedPaymentSession", transactionId);
    }

    public async Task LeavePaymentSession(string transactionId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, transactionId);
    }
}