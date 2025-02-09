using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_COMMAND.PERSISTENCE.Outbox;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Subscriptions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;
[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint; // Maybe can use more Rebus library

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
                continue;

            try
            {
                switch (domainEvent.GetType().Name)
                {
                    case nameof(DomainEvents.SubscriptionCreated):
                        var subscriptionCreated = JsonConvert.DeserializeObject<DomainEvents.SubscriptionCreated>(
                            outboxMessage.Content,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            });
                        await _publishEndpoint.Publish(subscriptionCreated, context.CancellationToken);
                        break;
                    case nameof(DomainEvents.SubscriptionUpdated):
                        var subscriptionUpdated = JsonConvert.DeserializeObject<DomainEvents.SubscriptionUpdated>(
                            outboxMessage.Content,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            });
                        await _publishEndpoint.Publish(subscriptionUpdated, context.CancellationToken);
                        break;
                    case nameof(DomainEvents.SubscriptionDeleted):
                        var subscriptionDeleted = JsonConvert.DeserializeObject<DomainEvents.SubscriptionDeleted>(
                            outboxMessage.Content,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            });
                        await _publishEndpoint.Publish(subscriptionDeleted, context.CancellationToken);
                        break;
                    case nameof(DomainEvents.SubscriptionStatusActivationChanged):
                        var subscriptionActivated =
                            JsonConvert.DeserializeObject<DomainEvents.SubscriptionStatusActivationChanged>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(subscriptionActivated, context.CancellationToken);
                        break;
                }

                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                outboxMessage.Error = ex.Message;
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}