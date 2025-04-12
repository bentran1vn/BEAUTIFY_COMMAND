using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;
using ProceduresDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Procedures.DomainEvents;
using ServicePromotionDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ServicePromotion.DomainEvents;
using ClinicServicesDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ClinicServices.DomainEvents;
using ClinicDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Clinic.DomainEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class TriggerOutbox : AggregateRoot<Guid>, IAuditableEntity
{
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public static TriggerOutbox RaiseActivatedActionEvent(Guid clinicId, bool isActive, bool isParent)
    {   
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };
        
        triggerOutbox.RaiseDomainEvent(new ClinicDomainEvent.ClinicBranchActivatedAction(Guid.NewGuid(),
            new ClinicEvent.InActivatedClinic(clinicId, isActive, isParent)));
        
        return triggerOutbox;
    }

    public static TriggerOutbox RaiseCreateServiceProcedureEvent(
        Guid procedureId, Guid serviceId, string name, string description,
        decimal maxPrice, decimal minPrice, decimal? discountMaxPrice, decimal? discountMinPrice,
        int stepIndex, ICollection<ProcedurePriceType> procedurePriceTypes)
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ProceduresDomainEvent.ProcedureCreated(
            Guid.NewGuid(),
            new ProcedureEvent.CreateProcedure(
                procedureId, serviceId, name, description, maxPrice, minPrice, discountMaxPrice,
                discountMinPrice, stepIndex, procedurePriceTypes.Select(
                    x => new ProcedureEvent.ProcedurePriceType(
                        x.Id, x.Name, x.Price, x.Duration, x.IsDefault
                    )).ToList()
            )));

        return triggerOutbox;
    }
    
    public static TriggerOutbox RaiseUpdateServiceProcedureEvent(
        Guid procedureId, Guid serviceId, string name, string description,
        decimal maxPrice, decimal minPrice, decimal? discountMaxPrice, decimal? discountMinPrice,
        int stepIndex, ICollection<ProcedurePriceType> procedurePriceTypes)
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ProceduresDomainEvent.ProcedureUpdate(
            Guid.NewGuid(),
            new ProcedureEvent.CreateProcedure(
                procedureId, serviceId, name, description, maxPrice, minPrice, discountMaxPrice,
                discountMinPrice, stepIndex, procedurePriceTypes.Select(
                    x => new ProcedureEvent.ProcedurePriceType(
                        x.Id, x.Name, x.Price, x.Duration, x.IsDefault
                    )).ToList()
            )));

        return triggerOutbox;
    }
    
    public static TriggerOutbox RaiseDeleteServiceProcedureEvent(
        Guid serviceId, Guid procedureId,
        decimal maxPrice, decimal minPrice,
        decimal discountMaxPrice, decimal discountMinPrice
        
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ProceduresDomainEvent.ProcedureDelete(
            Guid.NewGuid(),
            new ProcedureEvent.DeleteProcedure(
                procedureId, serviceId,
                maxPrice, minPrice, discountMaxPrice,
                discountMinPrice
            )));

        return triggerOutbox;
    }

    public static TriggerOutbox RaiseCreatePromotionEvent(
        Guid promotionId,
        Guid serviceId,
        string name,
        double discountPercent,
        string imageUrl,
        decimal discountMaxPrice,
        decimal discountMinPrice,
        DateTime startDay,
        DateTime endDate)
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ServicePromotionDomainEvent.ServicePromotionCreated(
            Guid.NewGuid(),
            new PromotionEvent.CreateServicePromotion(
                promotionId, serviceId, name, discountPercent, imageUrl, discountMaxPrice,
                discountMinPrice, startDay, endDate
            )));

        return triggerOutbox;
    }
    
    public static TriggerOutbox RaiseUpdatePromotionEvent(
        Guid promotionId,
        Guid serviceId,
        string name,
        double discountPercent,
        string imageUrl,
        decimal discountMaxPrice,
        decimal discountMinPrice,
        DateTime startDay,
        DateTime endDate,
        bool isActivated)
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ServicePromotionDomainEvent.ServicePromotionUpdated(
            Guid.NewGuid(),
            new PromotionEvent.UpdateServicePromotion(
                serviceId, promotionId , name, discountPercent, imageUrl, discountMaxPrice,
                discountMinPrice, startDay, endDate, isActivated
            )));

        return triggerOutbox;
    }
    
    public static TriggerOutbox RaiseDeletePromotionEvent(
        Guid promotionId,
        Guid serviceId
        )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ServicePromotionDomainEvent.ServicePromotionDeleted(
            Guid.NewGuid(),
            new PromotionEvent.RemoveServicePromotion(
                serviceId, promotionId 
            )));

        return triggerOutbox;
    }
    
    public static TriggerOutbox RaiseCreateClinicServiceEvent(
        Guid id, string name, string description,
        Clinic branding,
        ServiceMedia[] coverImage,
        Guid cateId, string cateName,
        string cateDescription, List<Clinic> clinics
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ClinicServicesDomainEvent.ClinicServiceCreated(
            Guid.NewGuid(),
            new ClinicServiceEvent.CreateClinicService(
                id, name, description, new ClinicServiceEvent.Clinic(branding.Id, branding.Name, branding.Email,
                    branding.City, branding.Address, branding.FullAddress, branding.District, branding.Ward, branding.PhoneNumber, branding.ProfilePictureUrl,
                    branding.IsParent, branding.ParentId),
                coverImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                
                new ClinicServiceEvent.Category(cateId, cateName, cateDescription),
                clinics.Select(x => new ClinicServiceEvent.Clinic(x.Id, x.Name, x.Email,
                    x.City, x.Address, x.FullAddress, x.District, x.Ward, x.PhoneNumber, x.ProfilePictureUrl,
                    x.IsParent, x.ParentId)).ToList()
            )));

        return triggerOutbox;
    }

    public static TriggerOutbox RaiseUpdateClinicServiceEvent(
        Guid id, string name, string description,
        ServiceMedia[] changeCoverImage, ServiceMedia[] changeDescriptionImage,
        Guid cateId, string cateName,
        string cateDescription, List<Clinic> clinics
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ClinicServicesDomainEvent.ClinicServiceUpdated(
            Guid.NewGuid(),
            new ClinicServiceEvent.UpdateClinicService(
                id, name, description,
                changeCoverImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                new ClinicServiceEvent.Category(cateId, cateName, cateDescription),
                clinics.Select(x => new ClinicServiceEvent.Clinic(x.Id, x.Name, x.Email,
                    x.City, x.Address, x.FullAddress, x.District, x.Ward, x.PhoneNumber, x.ProfilePictureUrl,
                    x.IsParent, x.ParentId)).ToList()
            )));

        return triggerOutbox;
    }
    
    public static TriggerOutbox RaiseDeleteClinicServiceEvent(
        Guid id
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ClinicServicesDomainEvent.ClinicServiceDeleted(
            Guid.NewGuid(),
            new ClinicServiceEvent.DeleteClinicService(
                id
            )));

        return triggerOutbox;
    }
}