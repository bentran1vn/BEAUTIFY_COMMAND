using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;
using ProceduresDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Procedures.DomainEvents;
using ServicePromotionDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ServicePromotion.DomainEvents;
using ClinicServicesDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ClinicServices.DomainEvents;
using ClinicDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Clinic.DomainEvents;
using FeedbackDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Feedback.DomainEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class TriggerOutbox : AggregateRoot<Guid>, IAuditableEntity
{
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public static TriggerOutbox RaiseActivatedActionEvent(Guid clinicId, bool isActive, bool isParent, Guid parentId)
    {   
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };
        
        triggerOutbox.RaiseDomainEvent(new ClinicDomainEvent.ClinicBranchActivatedAction(Guid.NewGuid(),
            new ClinicEvent.InActivatedClinic(clinicId, isActive, isParent, parentId)));
        
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
        double depositPercent,
        bool isRefundable,
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
                    branding.City ?? "", branding.Address ?? "", branding.FullAddress ?? ""
                    , branding.WorkingTimeStart ?? TimeSpan.Zero, branding.WorkingTimeEnd ?? TimeSpan.Zero,
                    branding.District ?? "", branding.Ward ?? "",
                    branding.PhoneNumber, branding.ProfilePictureUrl,
                    branding.IsParent, branding.ParentId),
                depositPercent, isRefundable,
                coverImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                
                new ClinicServiceEvent.Category(cateId, cateName, cateDescription),
                clinics.Select(x => new ClinicServiceEvent.Clinic(x.Id, x.Name, x.Email,
                    x.City ?? "", x.Address ?? "", x.FullAddress ?? "",x.WorkingTimeStart ?? TimeSpan.Zero,
                    x.WorkingTimeEnd ?? TimeSpan.Zero,
                    x.District ?? "", x.Ward ?? "", x.PhoneNumber, x.ProfilePictureUrl,
                    x.IsParent, x.ParentId)).ToList()
            )));

        return triggerOutbox;
    }

    public static TriggerOutbox RaiseUpdateClinicServiceEvent(
        Guid id, string name, string description,
        double depositPercent,
        bool isRefundable,
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
                depositPercent, isRefundable,
                changeCoverImage.Select(x => new ClinicServiceEvent.Image(
                    x.Id, x.IndexNumber, x.ImageUrl
                )).ToArray(),
                new ClinicServiceEvent.Category(cateId, cateName, cateDescription),
                clinics.Select(x => new ClinicServiceEvent.Clinic(x.Id, x.Name, x.Email,
                    x.City ?? "", x.Address ?? "", x.FullAddress ?? "", x.WorkingTimeStart ?? TimeSpan.Zero,
                    x.WorkingTimeEnd ?? TimeSpan.Zero, x.District ?? "", x.Ward ?? "",
                    x.PhoneNumber, x.ProfilePictureUrl,
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
    
    public class DoctorFeedback
    {
        public Guid FeedbackId { get; set; }
        public double NewRating { get; set; }
        public Guid DoctorId { get; set; }
        public string Content { get; set; }
    }

    public static TriggerOutbox CreateFeedbackEvent(
        Guid feedbackId, Guid serviceId, ICollection<string> images,
        string content, double rating, User user, DateTimeOffset createdAt,
        double newRating, List<DoctorFeedback> doctorFeedbacks
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new FeedbackDomainEvent.CreateFeedback(
            Guid.NewGuid(),
            new FeedbackEvent.CreateFeedback
            {
                FeedbackId = feedbackId,
                ServiceId = serviceId,
                Images = images,
                Content = content,
                Rating = rating,
                NewRating = newRating,
                User = new FeedbackEvent.User
                {
                    Id = user.Id,
                    Avatar = user.ProfilePicture ?? "",
                    PhoneNumber = user.PhoneNumber ?? "",
                    FullName = user.FullName ?? "",
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address ?? ""
                },
                CreatedAt = createdAt ,
                DoctorFeedbacks = doctorFeedbacks.Select(x => new FeedbackEvent.DoctorFeedback
                {
                    FeedbackId = x.FeedbackId,
                    NewRating = x.NewRating,
                    DoctorId = x.DoctorId,
                    Content = x.Content
                }).ToList()
            }));

        return triggerOutbox;
    }
    
    public static TriggerOutbox UpdateFeedbackEvent(
        Guid feedbackId, Guid serviceId, ICollection<string> images,
        string content, double rating, DateTimeOffset updateAt, double newRating,
        List<DoctorFeedback> doctorFeedbacks
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new FeedbackDomainEvent.UpdateFeedback(
            Guid.NewGuid(),
            new FeedbackEvent.UpdateFeedback()
            {
                FeedbackId = feedbackId,
                ServiceId = serviceId,
                Images = images,
                Content = content,
                Rating = rating,
                UpdateAt = updateAt,
                NewRating = newRating,
                DoctorFeedbacks = doctorFeedbacks.Select(x => new FeedbackEvent.DoctorFeedback
                {
                    FeedbackId = x.FeedbackId,
                    NewRating = x.NewRating,
                    DoctorId = x.DoctorId,
                    Content = x.Content
                }).ToList()
            }));

        return triggerOutbox;
    }
    
    public static TriggerOutbox DisplayFeedbackEvent(
        Guid feedbackId, Guid serviceId, bool isView
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new FeedbackDomainEvent.ViewActionFeedback(
            Guid.NewGuid(),
            new FeedbackEvent.ViewActionFeedback()
            {
                FeedbackId = feedbackId,
                ServiceId = serviceId,
                IsView = isView
            }));

        return triggerOutbox;
    }
    
    public static TriggerOutbox UpdateBranchEvent(
        bool isParent, Clinic clinic
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ClinicDomainEvent.ClinicUpdated(
            Guid.NewGuid(),
            new ClinicEvent.ClinicUpdated(
                new ClinicEvent.Clinic(
                clinic.Id, clinic.Name, clinic.Email, clinic.City ?? "", clinic.Address ?? "",
                clinic.FullAddress ?? "", clinic.WorkingTimeStart ?? TimeSpan.Zero, clinic.WorkingTimeEnd ?? TimeSpan.Zero,
                clinic.District ?? "", clinic.Ward ?? "", clinic.PhoneNumber, clinic.ProfilePictureUrl,
                clinic.IsParent, clinic.ParentId
                ),
            isParent)
            )
        );

        return triggerOutbox;
    }
    
    public static TriggerOutbox DeleteBranchEvent(
        bool isParent, Clinic clinic
    )
    {
        var triggerOutbox = new TriggerOutbox
        {
            Id = Guid.NewGuid()
        };

        triggerOutbox.RaiseDomainEvent(new ClinicDomainEvent.ClinicDeleted(
                Guid.NewGuid(),
                new ClinicEvent.ClinicDeleted(
                    clinic.Id,
                    isParent,
                    clinic.ParentId
                )
            )
        );

        return triggerOutbox;
    }
}