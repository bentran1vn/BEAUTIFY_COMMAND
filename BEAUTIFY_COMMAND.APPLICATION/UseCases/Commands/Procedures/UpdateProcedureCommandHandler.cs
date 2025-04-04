using MongoDB.Driver.Linq;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Procedures;

public class UpdateProcedureCommandHandler(
        IRepositoryBase<Service, Guid> clinicServiceRepository,
        IRepositoryBase<Procedure, Guid> procedureServiceRepository,
        IRepositoryBase<ProcedurePriceType, Guid> procedurePriceTypeServiceRepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository
    ) : ICommandHandler<CONTRACT.Services.Procedures.Commands.UpdateProcedureCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Procedures.Commands.UpdateProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedures = await procedureServiceRepository.FindAll(
            x => x.ServiceId.Equals(request.ServiceId) &&
                 !x.IsDeleted).Include(x => x.ProcedurePriceTypes).ToListAsync(cancellationToken);
        
        var isExisted = procedures.FirstOrDefault(x => x.Id.Equals(request.ProcedureId));
        
        if(isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Procedure not found !"));
        
        if(request.ProcedurePriceTypes.Count(x => x.IsDefault) > 1)
            return Result.Failure(new Error("400", "Only one price type can be default !"));
        
        isExisted.Name = request.Name;
        isExisted.Description = request.Description;
        
        if (procedures.Max(x => x.StepIndex) < request.StepIndex ||
            request.StepIndex < procedures.Min(x => x.StepIndex))
        {
            return Result.Failure(new Error("400", "Step index is out of range !"));
        }
        
        if(isExisted.StepIndex != request.StepIndex)
        {
            List<Procedure> proceduresToUpdate = new List<Procedure>();
            
            // Jump back
            if(isExisted.StepIndex > request.StepIndex)
            {
                proceduresToUpdate = procedures
                    .Where(x => x.StepIndex >= request.StepIndex &&
                        x.StepIndex < isExisted.StepIndex).ToList();
                
                foreach (var item in proceduresToUpdate)
                {
                    item.StepIndex += 1;
                }
            }
            
            // Jump next
            if(isExisted.StepIndex < request.StepIndex)
            {
                proceduresToUpdate = procedures
                    .Where(x => x.StepIndex <= request.StepIndex &&
                                x.StepIndex > isExisted.StepIndex).ToList();
                foreach (var item in proceduresToUpdate)
                {
                    item.StepIndex -= 1;
                }
            }

            if(proceduresToUpdate.Any()) 
                procedureServiceRepository.UpdateRange(proceduresToUpdate);
            
            isExisted.StepIndex = request.StepIndex;
        }
        
        // procedureServiceRepository.Update(isExisted);

        foreach (var item in isExisted.ProcedurePriceTypes)
        {
            item.IsDeleted = true;
        }
        
        procedurePriceTypeServiceRepository.UpdateRange(isExisted.ProcedurePriceTypes);
        
        var newProcedurePriceTypes = request.ProcedurePriceTypes.Select(x => new ProcedurePriceType
        {
            Id = Guid.NewGuid(),
            Name = x.Name,
            Price = x.Price,
            Duration = x.Duration,
            IsDefault = x.IsDefault,
            ProcedureId = isExisted.Id
        }).ToList();
        
        procedurePriceTypeServiceRepository.AddRange(newProcedurePriceTypes);
        
        var service = await clinicServiceRepository.FindByIdAsync(request.ServiceId, cancellationToken,
            x => x.Promotions);
        
        var discountPercent = service!.Promotions?.FirstOrDefault(
            x => x.ServiceId.Equals(request.ServiceId) &&
                 !x.IsDeleted && x.IsActivated && x.LivestreamRoom == null)?.DiscountPercent;
        
        var lowestPrice = service.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Min(pt => pt.Price)
                : 0) ?? 0;

        var highestPrice = service.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Max(pt => pt.Price)
                : 0) ?? 0;
        
        var triggerOutbox = TriggerOutbox.RaiseUpdateServiceProcedureEvent(
            isExisted.Id, (Guid)isExisted.ServiceId, isExisted.Name, isExisted.Description,
            highestPrice, lowestPrice, highestPrice - (decimal?)discountPercent * highestPrice,
            lowestPrice - (decimal?)discountPercent * lowestPrice, isExisted.StepIndex,
            newProcedurePriceTypes.ToList());
        
        triggerOutboxRepository.Add(triggerOutbox);
        
        return Result.Success("Update Service's Procedure Successfully");
    }
}