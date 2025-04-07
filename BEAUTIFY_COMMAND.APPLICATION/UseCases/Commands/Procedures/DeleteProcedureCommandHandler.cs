using MongoDB.Driver.Linq;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Procedures;
public class
    DeleteProcedureCommandHandler : ICommandHandler<CONTRACT.Services.Procedures.Commands.DeleteProcedureCommand>
{
    private readonly IRepositoryBase<Procedure, Guid> _procedureRepository;
    private readonly IRepositoryBase<TriggerOutbox, Guid> _triggerOutboxRepository;
    private readonly IRepositoryBase<Service, Guid> _serviceRepository;

    public DeleteProcedureCommandHandler(IRepositoryBase<Procedure, Guid> procedureRepository, IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository, IRepositoryBase<Service, Guid> serviceRepository)
    {
        _procedureRepository = procedureRepository;
        _triggerOutboxRepository = triggerOutboxRepository;
        _serviceRepository = serviceRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Procedures.Commands.DeleteProcedureCommand request,
        CancellationToken cancellationToken)
    {
        var query = _procedureRepository.FindAll(x => x.Id == request.Id).AsTracking();

        var isExisted = await query.FirstOrDefaultAsync(cancellationToken);

        if (isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Procedure not found "));
        
        var service = await _serviceRepository.FindAll(
                x => x.Id.Equals(isExisted.ServiceId!) && x.IsDeleted == false)
            .Include(x => x.Promotions)
            .Include(x => x.Procedures)
            .ThenInclude(x => x.ProcedurePriceTypes)
            .FirstOrDefaultAsync(cancellationToken);
        
        if(service == null) return Result.Failure(new Error("404", "Service not found"));
        
        var procedureUpdates = service.Procedures
            .Where(x => x.StepIndex > isExisted.StepIndex && x.IsDeleted == false)
            .ToList();
        
        foreach (var item in procedureUpdates)
        {
            item.StepIndex -= 1;
        }
        
        _procedureRepository.UpdateRange(procedureUpdates);
        _procedureRepository.Remove(isExisted);
        
        var procedureTotal = service.Procedures?.Where(x => x.Id != request.Id).ToList();
        var promotionTotal = service.Promotions?.ToList();
        
        var lowestPrice = procedureTotal?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Min(pt => pt.Price)
                : 0) ?? 0;

        var highestPrice = procedureTotal?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Max(pt => pt.Price)
                : 0) ?? 0;

        var discountPercent = promotionTotal?.FirstOrDefault(x =>
            x.IsActivated && x.ServiceId.Equals(service.Id) &&
            !x.IsDeleted  && x.LivestreamRoom == null)?.DiscountPercent ?? 0;
        
        var trigger = TriggerOutbox.RaiseDeleteServiceProcedureEvent(
            (Guid)isExisted.ServiceId!, isExisted.Id,
            highestPrice, lowestPrice,
            highestPrice - (decimal)discountPercent * highestPrice,
            lowestPrice - (decimal)discountPercent * lowestPrice);
        
        _triggerOutboxRepository.Add(trigger);

        return Result.Success("Procedure deleted");
    }
}