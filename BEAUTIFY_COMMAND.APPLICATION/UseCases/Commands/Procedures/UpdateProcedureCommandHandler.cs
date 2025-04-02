using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Procedures;

public class UpdateProcedureCommandHandler(
        IRepositoryBase<Service, Guid> clinicServiceRepository,
        IRepositoryBase<Procedure, Guid> procedureServiceRepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository
    ) : ICommandHandler<CONTRACT.Services.Procedures.Commands.UpdateProcedureCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Procedures.Commands.UpdateProcedureCommand request, CancellationToken cancellationToken)
    {
        var procedures = await procedureServiceRepository.FindAll(
            x => x.ServiceId.Equals(request.ServiceId) &&
                 !x.IsDeleted).ToListAsync(cancellationToken);
        
        var isExisted = procedures.FirstOrDefault(x => x.Id.Equals(request.ProcedureId));
        
        if(isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Procedure not found !"));
        
        if (procedures.Any(p => p.StepIndex == request.StepIndex && p.IsDeleted == false && p.Id != request.ProcedureId))
            return Result.Failure(new Error("400", "Step Index Exist !"));
        
        isExisted.Name = request.Name;
        isExisted.Description = request.Description;
        isExisted.StepIndex = request.StepIndex;
        isExisted.ServiceId = request.ServiceId;
        isExisted.ProcedurePriceTypes = request.ProcedurePriceTypes.Select(x => new ProcedurePriceType
        {
            Id = Guid.NewGuid(),
            Name = x.Name,
            Price = x.Price,
            Duration = x.Duration,
            IsDefault = x.IsDefault,
            ProcedureId = isExisted.Id
        }).ToList();
        
        procedureServiceRepository.Update(isExisted);
        
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
            isExisted.ProcedurePriceTypes);
        
        triggerOutboxRepository.Add(triggerOutbox);
        
        return Result.Success("Update Service's Procedure Successfully");
    }
}