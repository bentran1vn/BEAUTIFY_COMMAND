using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_COMMAND.PERSISTENCE.Outbox;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;
using SubscriptionsDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Subscriptions.DomainEvents;
// using PostgreMigrateDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CommandConverts.DomainEvents;
using ClinicServiceDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ClinicServices.DomainEvents;
using ProcedureDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Procedures.DomainEvents;
using WorkingScheduleDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.WorkingSchedules.DomainEvents;
using ServicePromotionDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ServicePromotion.DomainEvents;
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
                        var subscriptionCreated =
                            JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(subscriptionCreated, context.CancellationToken);
                        break;
                    case nameof(SubscriptionsDomainEvent.SubscriptionUpdated):
                        var subscriptionUpdated =
                            JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionUpdated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(subscriptionUpdated, context.CancellationToken);
                        break;
                    case nameof(SubscriptionsDomainEvent.SubscriptionDeleted):
                        var subscriptionDeleted =
                            JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionDeleted>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(subscriptionDeleted, context.CancellationToken);
                        break;
                    // case nameof(PostgreMigrateDomainEvent.PostgreMigrate):
                    //     var postgreMigrate =
                    //         JsonConvert.DeserializeObject<PostgreMigrateDomainEvent.PostgreMigrate>(
                    //             outboxMessage.Content,
                    //             new JsonSerializerSettings
                    //             {
                    //                 TypeNameHandling = TypeNameHandling.All
                    //             });
                    //     await _publishEndpoint.Publish(postgreMigrate, context.CancellationToken);
                    //     break;
                    case nameof(SubscriptionsDomainEvent.SubscriptionStatusActivationChanged):
                        var subscriptionStatusActivationChanged =
                            JsonConvert.DeserializeObject<SubscriptionsDomainEvent.SubscriptionStatusActivationChanged>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(subscriptionStatusActivationChanged, context.CancellationToken);
                        break;
                    case nameof(ClinicServiceDomainEvent.ClinicServiceCreated):
                        var clinicServiceCreated =
                            JsonConvert.DeserializeObject<ClinicServiceDomainEvent.ClinicServiceCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(clinicServiceCreated, context.CancellationToken);
                        break;
                    case nameof(ClinicServiceDomainEvent.ClinicServiceUpdated):
                        var clinicServiceUpdated =
                            JsonConvert.DeserializeObject<ClinicServiceDomainEvent.ClinicServiceUpdated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(clinicServiceUpdated, context.CancellationToken);
                        break;
                    case nameof(ProcedureDomainEvent.ProcedureCreated):
                        var procedureCreated =
                            JsonConvert.DeserializeObject<ProcedureDomainEvent.ProcedureCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(procedureCreated, context.CancellationToken);
                        break;

                    case nameof(WorkingScheduleDomainEvent.WorkingScheduleCreated):
                        var workingScheduleCreated =
                            JsonConvert.DeserializeObject<WorkingScheduleDomainEvent.WorkingScheduleCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(workingScheduleCreated, context.CancellationToken);
                        break;
                    case nameof(WorkingScheduleDomainEvent.WorkingScheduleDeleted):
                        var workingScheduleDeleted =
                            JsonConvert.DeserializeObject<WorkingScheduleDomainEvent.WorkingScheduleDeleted>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(workingScheduleDeleted, context.CancellationToken);
                        break;
                    case nameof(WorkingScheduleDomainEvent.WorkingScheduleUpdated):
                        var workingScheduleUpdated =
                            JsonConvert.DeserializeObject<WorkingScheduleDomainEvent.WorkingScheduleUpdated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(workingScheduleUpdated, context.CancellationToken);
                        break;
                    case nameof(ServicePromotionDomainEvent.ServicePromotionCreated):
                        var servicePromotionCreated =
                            JsonConvert.DeserializeObject<ServicePromotionDomainEvent.ServicePromotionCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await _publishEndpoint.Publish(servicePromotionCreated, context.CancellationToken);
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