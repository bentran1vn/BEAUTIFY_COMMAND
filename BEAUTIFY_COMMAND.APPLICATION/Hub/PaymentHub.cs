using BEAUTIFY_COMMAND.PERSISTENCE;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Storage;

namespace BEAUTIFY_COMMAND.APPLICATION.Hub;
public class PaymentHub(IRepositoryBase<SystemTransaction, Guid> repositoryBase, ApplicationDbContext dbContext)
    : Microsoft.AspNetCore.SignalR.Hub
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

    public async Task CancelPaymentSession(Guid transactionId)
    {
        var trans = await repositoryBase.FindByIdAsync(transactionId);
        if (trans != null)
        {
            trans.Status = 2;
            repositoryBase.Update(trans);
        }

        await dbContext.SaveChangesAsync();
    }
}