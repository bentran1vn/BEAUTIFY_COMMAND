using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

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
            x => x.Promotions);

        if (isExisted == null || isExisted.IsDeleted) return Result.Failure(new Error("404", "Services not found !"));

        if (isExisted.Procedures != null &&
            isExisted.Procedures.Any(p => p.StepIndex == request.StepIndex && p.IsDeleted == false))
            return Result.Failure(new Error("400", "Step Index Exist !"));

        if (request.ProcedurePriceTypes == null || !request.ProcedurePriceTypes.Any())
            return Result.Failure(new Error("400", "No Price Types !"));

        var procedure = new Procedure
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            StepIndex = request.StepIndex,
            ServiceId = request.ClinicServiceId
        };

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

        var lowestPrice = isExisted.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Min(pt => pt.Price)
                : 0) ?? 0;

        var highestPrice = isExisted.Procedures?.Sum(procedure =>
            procedure.ProcedurePriceTypes.Any()
                ? procedure.ProcedurePriceTypes.Max(pt => pt.Price)
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