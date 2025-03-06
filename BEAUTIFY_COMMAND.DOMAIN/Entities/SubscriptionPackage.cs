using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Subscriptions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class SubscriptionPackage : AggregateRoot<Guid>, IAuditableEntity
{
    [MaxLength(50)] public required string Name { get; set; }
    [MaxLength(200)] public required string Description { get; set; }
    [Column(TypeName = "decimal(18,2)")] public required decimal Price { get; set; }
    public required int Duration { get; set; }
    public bool IsActivated { get; set; }
    public int LimitBranch { get; set; }
    public int LimitLiveStream { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }


    public static SubscriptionPackage Create(
        string name,
        string description,
        decimal price,
        int duration,
        int limitBranch,
        int limitLiveStream)
    {
        var subscription = new SubscriptionPackage
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Price = price,
            Duration = duration,
            IsActivated = false,
            CreatedOnUtc = DateTimeOffset.UtcNow,
            LimitBranch = limitBranch,
            LimitLiveStream = limitLiveStream
        };

        // Raise the domain event
        subscription.RaiseDomainEvent(new DomainEvents.SubscriptionCreated(
            Guid.NewGuid(),
            new EntityEvent.SubscriptionEntity
            {
                Id = subscription.Id,
                Name = subscription.Name,
                Description = subscription.Description,
                Price = subscription.Price,
                Duration = subscription.Duration,
                IsActivated = subscription.IsActivated,
                IsDeleted = false,
                Created = subscription.CreatedOnUtc,
                ModifiedOnUtc = null,
                LimitBranch = subscription.LimitBranch,
                LimitLiveStream = subscription.LimitLiveStream
            }));

        return subscription;
    }

    public void UpdateSubscription(
        string name,
        string description,
        decimal price,
        int duration,
        int limitBranch,
        int limitLiveStream)
    {
        Name = name;
        Description = description;
        Price = price;
        Duration = duration;
        ModifiedOnUtc = DateTimeOffset.UtcNow;
        LimitBranch = limitBranch;
        LimitLiveStream = limitLiveStream;


        // Raise domain event for update
        RaiseDomainEvent(new DomainEvents.SubscriptionUpdated(
            Guid.NewGuid(),
            new EntityEvent.SubscriptionEntity
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                Duration = Duration,
                IsActivated = IsActivated,
                IsDeleted = false,
                Created = CreatedOnUtc,
                ModifiedOnUtc = ModifiedOnUtc,
                LimitBranch = LimitBranch,
                LimitLiveStream = LimitLiveStream
            }));
    }

    public void DeleteSubscription()
    {
        IsDeleted = true;
        ModifiedOnUtc = DateTimeOffset.UtcNow;

        // Raise domain event for delete
        RaiseDomainEvent(new DomainEvents.SubscriptionDeleted(
            Guid.NewGuid(),
            Id));
    }

    public void ChangeStatusSubscription()
    {
        IsActivated = !IsActivated;
        ModifiedOnUtc = DateTimeOffset.UtcNow;

        // Raise domain event for change status
        RaiseDomainEvent(new DomainEvents.SubscriptionStatusActivationChanged(
            Guid.NewGuid(),
            Id
        ));
    }
}