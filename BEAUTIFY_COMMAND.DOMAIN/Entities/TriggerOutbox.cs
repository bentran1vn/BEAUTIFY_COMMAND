using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Procedures;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;

public class TriggerOutbox: AggregateRoot<Guid>, IAuditableEntity
{
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
    
    public static TriggerOutbox RaiseCreateServiceProcedureEvent(Guid procedureId, Guid serviceId, string name, string description,
        int stepIndex, string[] coverImage, ICollection<ProcedurePriceType> procedurePriceTypes)
    {
        var triggerOutbox = new TriggerOutbox()
        {
            Id = Guid.NewGuid(),
        };
        
        triggerOutbox.RaiseDomainEvent(new DomainEvents.ProcedureCreated(
            Guid.NewGuid(),
            new ProcedureEvent.CreateProcedure(
                procedureId, serviceId, name, description, stepIndex,
                coverImage, procedurePriceTypes.Select(
                    x => new ProcedureEvent.ProcedurePriceType(
                        x.Id, x.Name, x.Price
                    )).ToList()
            )));
        
        return triggerOutbox;
    }
}