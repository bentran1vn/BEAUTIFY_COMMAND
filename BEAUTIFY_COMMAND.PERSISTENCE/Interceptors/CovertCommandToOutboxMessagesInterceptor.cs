using BEAUTIFY_COMMAND.PERSISTENCE.Outbox;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CommandConverts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Interceptors;

public sealed class CovertCommandToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entity = dbContext.ChangeTracker.Entries()
            .Where(
                e => e.State == EntityState.Added
                     || e.State == EntityState.Modified
                     || e.State == EntityState.Deleted)
            .Where(e => e.Metadata.ClrType != typeof(OutboxMessage)).ToList();;
        
        var outboxMessages = entity.Select(entry =>
        {
            var entityType = entry.Entity.GetType();
            var entryEntity = entry.Entity;
            var primaryKey = GetPrimaryKeyValue(entry);
            var operation = entry.State switch
            {
                EntityState.Added => "Created",
                EntityState.Modified => "Updated",
                EntityState.Deleted => "Deleted",
                _ => throw new InvalidOperationException("Unknown state")
            };
            
            
            return new OutboxMessage()
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = nameof(DomainEvents.PostgreMigrate),
                Content = JsonConvert.SerializeObject(
                    new DomainEvents.PostgreMigrate(
                        Guid.NewGuid(),
                        entityType.Name,
                        primaryKey,
                        operation,
                        JsonConvert.SerializeObject(
                            RemoveNavigationProperties(dbContext ,entryEntity, entityType), // ✅ Remove navigation properties before serialization
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            })
                    ),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                )
            };
        }).Where(x => x != null) // 🔹 Remove null values
        .ToList();;
        
        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
        
        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    private object? GetPrimaryKeyValue(EntityEntry entry)
    {
        var key = entry.Metadata.FindPrimaryKey();
        return key?.Properties.Select(p => entry.Property(p.Name).CurrentValue).FirstOrDefault();
    }
    
    private object RemoveNavigationProperties(DbContext context, object entity, Type entityType)
    {
        // Create a deep copy to prevent modifying the tracked entity
        var clone = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(entity), entityType);

        var entityTypeModel = context.Model.FindEntityType(entityType);
        if (entityTypeModel == null)
        {
            return clone!;
        }

        var navigationProperties = entityTypeModel.GetNavigations()
            .Select(n => n.Name)
            .ToList();

        foreach (var propertyName in navigationProperties)
        {
            var property = entityType.GetProperty(propertyName);
            if (property == null) continue;

            var propertyValue = property.GetValue(clone);

            if (propertyValue == null) continue;

            if (property.PropertyType.IsGenericType) // Collection navigation property
            {
                // If it's a collection, remove all elements
                property.SetValue(clone, null);
            }
            else
            {
                // If it's a reference, set it to null
                property.SetValue(clone, null);
            }
        }

        return clone!;
    }
}