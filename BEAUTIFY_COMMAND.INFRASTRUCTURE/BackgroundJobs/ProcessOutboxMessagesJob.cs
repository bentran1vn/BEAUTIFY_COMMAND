using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_COMMAND.PERSISTENCE.Outbox;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;
using SubscriptionsDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Subscriptions.DomainEvents;
using PostgreMigrateDomainEvent =  BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CommandConverts.DomainEvents;
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
                    case nameof(SubscriptionsDomainEvent.SubscriptionCreated):
                        var subscriptionCreated = JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionCreated>(
                            outboxMessage.Content,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            });
                        await _publishEndpoint.Publish(subscriptionCreated, context.CancellationToken);
                        break;
                    case nameof(SubscriptionsDomainEvent.SubscriptionUpdated):
                        var subscriptionUpdated = JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionUpdated>(
                            outboxMessage.Content,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            });
                        await _publishEndpoint.Publish(subscriptionUpdated, context.CancellationToken);
                        break;
                    case nameof(SubscriptionsDomainEvent.SubscriptionDeleted):
                        var subscriptionDeleted = JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionDeleted>(
                            outboxMessage.Content,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            });
                        await _publishEndpoint.Publish(subscriptionDeleted, context.CancellationToken);
                        break;
                    case nameof(SubscriptionsDomainEvent.SubscriptionStatusActivationChanged):
                        var subscriptionActivated =
                            JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionStatusActivationChanged>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(subscriptionActivated, context.CancellationToken);
                        break;
                    case nameof(PostgreMigrateDomainEvent.PostgreMigrate):
                        var postgreMigrate =
                            JsonConvert.DeserializeObject<PostgreMigrateDomainEvent.PostgreMigrate>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(postgreMigrate, context.CancellationToken);
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