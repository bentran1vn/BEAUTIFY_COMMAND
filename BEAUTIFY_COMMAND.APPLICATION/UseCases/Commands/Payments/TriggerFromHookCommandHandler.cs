using BEAUTIFY_COMMAND.APPLICATION.Hub;
using Microsoft.AspNetCore.SignalR;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Payments;

public class TriggerFromHookCommandHandler: ICommandHandler<CONTRACT.Services.Payments.Commands.TriggerFromHookCommand>
{
    private readonly IRepositoryBase<SystemTransaction, Guid> _systemTransactionRepository;
    private readonly IHubContext<PaymentHub> _hubContext;

    public TriggerFromHookCommandHandler(IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository, IHubContext<PaymentHub> hubContext)
    {
        _systemTransactionRepository = systemTransactionRepository;
        _hubContext = hubContext;
    }

    public async Task<Result> Handle(CONTRACT.Services.Payments.Commands.TriggerFromHookCommand request, CancellationToken cancellationToken)
    {
        if (request.Type == 0)
        {
            var tran = await _systemTransactionRepository.FindByIdAsync(request.Id, cancellationToken);

            if (tran == null || tran.IsDeleted)
            {
                return Result.Failure(new Error("404", "Transaction not found"));
            }
        
            if(tran.Status != 0)
            {
                return Result.Failure(new Error("500", "Transaction already handler"));
            }

            if (tran.Amount != request.TransferAmount)
            {
                return Result.Failure(new Error("500", "Transaction Amount invalid"));
            }
            
            if (tran.TransactionDate > DateTimeOffset.Now)
            {
                return Result.Failure(new Error("500", "Transaction Date invalid"));
            }
            
            tran.Status = 1;
            
            await _hubContext.Clients.Group(tran.Id.ToString()).SendAsync("ReceivePaymentStatus", true, cancellationToken);
        }
        
        return Result.Success("Handler successfully triggered.");
    }
}