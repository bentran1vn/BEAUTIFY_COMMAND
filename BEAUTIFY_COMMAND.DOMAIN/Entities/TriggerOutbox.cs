using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;
using ProceduresDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Procedures.DomainEvents;
using ServicePromotionDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ServicePromotion.DomainEvents;
using ClinicServicesDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ClinicServices.DomainEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class TriggerOutbox : AggregateRoot<Guid>, IAuditableEntity
{
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public static TriggerOutbox RaiseCreateServiceProcedureEvent(
        Guid procedureId, Guid serviceId, string name, string description,
        decimal maxPrice, decimal minPrice, decimal? discountMaxPrice, decimal? discountMinPrice,
        int stepIndex, string[] coverImage, ICollection<ProcedurePriceType> procedurePriceTypes)
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ProceduresDomainEvent.ProcedureCreated(
            Guid.NewGuid(),
            new ProcedureEvent.CreateProcedure(
                procedureId, serviceId, name, description, maxPrice, minPrice, discountMaxPrice,
                discountMinPrice, stepIndex, coverImage, procedurePriceTypes.Select(
                    x => new ProcedureEvent.ProcedurePriceType(
                        x.Id, x.Name, x.Price, x.Duration, x.IsDefault
                    )).ToList()
            )));

        return triggerOutbox;
    }

    public static TriggerOutbox RaiseCreatePromotionEvent(
        Guid PromotionId,
        Guid ServiceId,
        string Name,
        double DiscountPercent,
        string ImageUrl,
        decimal DiscountMaxPrice,
        decimal DiscountMinPrice,
        DateTime StartDay,
        DateTime EndDate)
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ServicePromotionDomainEvent.ServicePromotionCreated(
            Guid.NewGuid(),
            new PromotionEvent.CreateServicePromotion(
                PromotionId, ServiceId, Name, DiscountPercent, ImageUrl, DiscountMaxPrice,
                DiscountMinPrice, StartDay, EndDate
            )));

        return triggerOutbox;
    }

    public static TriggerOutbox RaiseCreateClinicServiceEvent(
        Guid Id, string Name, string Description,
        ServiceMedia[] CoverImage, ServiceMedia[] DescriptionImage,
        Guid CateId, string CateName,
        string CateDescription, List<Clinic> clinics
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ClinicServicesDomainEvent.ClinicServiceCreated(
            Guid.NewGuid(),
            new ClinicServiceEvent.CreateClinicService(
                Id, Name, Description,
                CoverImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                DescriptionImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                new ClinicServiceEvent.Category(CateId, CateName, CateDescription),
                clinics.Select(x => new ClinicServiceEvent.Clinic(x.Id, x.Name, x.Email,
                    x.City, x.Address, x.FullAddress, x.District, x.Ward, x.PhoneNumber, x.ProfilePictureUrl,
                    x.IsParent, x.ParentId)).ToList()
            )));

        return triggerOutbox;
    }

    public static TriggerOutbox RaiseUpdateClinicServiceEvent(
        Guid Id, string Name, string Description,
        ServiceMedia[] ChangeCoverImage, ServiceMedia[] ChangeDescriptionImage,
        Guid CateId, string CateName,
        string CateDescription, List<Clinic> clinics
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ClinicServicesDomainEvent.ClinicServiceUpdated(
            Guid.NewGuid(),
            new ClinicServiceEvent.UpdateClinicService(
                Id, Name, Description,
                ChangeCoverImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                ChangeDescriptionImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                new ClinicServiceEvent.Category(CateId, CateName, CateDescription),
                clinics.Select(x => new ClinicServiceEvent.Clinic(x.Id, x.Name, x.Email,
                    x.City, x.Address, x.FullAddress, x.District, x.Ward, x.PhoneNumber, x.ProfilePictureUrl,
                    x.IsParent, x.ParentId)).ToList()
            )));

        return triggerOutbox;
    }
}