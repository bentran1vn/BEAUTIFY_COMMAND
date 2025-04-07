namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Procedures;
public class
    CreateProcedureCommandHandler(
        IRepositoryBase<Service, Guid> clinicServiceRepository,
        IRepositoryBase<Procedure, Guid> procedureServiceRepository,
        IRepositoryBase<ProcedurePriceType, Guid> procedurePriceTypeServiceRepository,
        IRepositoryBase<TriggerOutbox, Guid> triggerOutboxRepository)
    : ICommandHandler<CONTRACT.Services.Procedures.Commands.CreateProcedureCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Procedures.Commands.CreateProcedureCommand request,
        CancellationToken cancellationToken)
    {
        var isExisted = await clinicServiceRepository.FindByIdAsync(request.ClinicServiceId, cancellationToken,
            x => x.Promotions, x => x.Procedures);

        if (isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Services not found !"));

        if (!request.ProcedurePriceTypes.Any())
            return Result.Failure(new Error("400", "No Price Types !"));
        
        if(request.ProcedurePriceTypes.Count(x => x.IsDefault) > 1)
            return Result.Failure(new Error("400", "Only one price type can be default !"));

        Procedure? procedure = null;
        
        if (request.StepIndex == null)
        {
            var nextStepIndex = isExisted.Procedures?.Any() == true ? isExisted.Procedures.Max(x => x.StepIndex) + 1 : 1;
            
            procedure = new Procedure
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StepIndex = nextStepIndex,
                ServiceId = request.ClinicServiceId
            };
        }
        else
        {
            var existIndex = isExisted.Procedures?.FirstOrDefault(
                x => x.StepIndex == request.StepIndex && x.IsDeleted == false
            );
            
            int? indexToAdd = null;
            
            if (existIndex != null)
            {
                var procedures = isExisted.Procedures?.Where(x => x.StepIndex >= request.StepIndex).ToList();
                if (procedures != null)
                {
                    foreach (var item in procedures)
                    {
                        item.StepIndex += 1;
                    }
                    procedureServiceRepository.UpdateRange(procedures);
                }
                indexToAdd = request.StepIndex;
            }
            else
            {
                indexToAdd = isExisted.Procedures?.Any() == true ? isExisted.Procedures?.Max(x => x.StepIndex) + 1 : 1;
            }
            
            procedure = new Procedure
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StepIndex = (int)indexToAdd,
                ServiceId = request.ClinicServiceId
            };
        }

        procedureServiceRepository.Add(procedure);

        var procedurePriceTypes = request.ProcedurePriceTypes.Select(x => new ProcedurePriceType
        {
            Id = Guid.NewGuid(),
            Name = x.Name,
            Price = x.Price,
            Duration = x.Duration,
            IsDefault = x.IsDefault,
            ProcedureId = procedure.Id
        });

        var priceTypes = procedurePriceTypes.ToList();

        procedurePriceTypeServiceRepository.AddRange(priceTypes);

        var lowestPrice = isExisted.Procedures?.Where(x => !x.IsDeleted).Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Where(x => !x.IsDeleted).Min(pt => pt.Price)
                : 0) ?? 0;

        var highestPrice = isExisted.Procedures?.Where(x => !x.IsDeleted).Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Where(x => !x.IsDeleted).Max(pt => pt.Price)
                : 0) ?? 0;

        var discountPercent = isExisted.Promotions?.FirstOrDefault(x =>
            x.IsActivated && x.ServiceId.Equals(request.ClinicServiceId) &&
                          !x.IsDeleted  && x.LivestreamRoom == null)?.DiscountPercent;

        var trigger = TriggerOutbox.RaiseCreateServiceProcedureEvent(
            procedure.Id, isExisted.Id, procedure.Name, procedure.Description,
            highestPrice, lowestPrice,
            highestPrice - (decimal?)discountPercent * highestPrice,
            lowestPrice - (decimal?)discountPercent * lowestPrice,
            procedure.StepIndex, priceTypes);

        triggerOutboxRepository.Add(trigger);

        return Result.Success("Create Service's Procedure Successfully");
    }
}