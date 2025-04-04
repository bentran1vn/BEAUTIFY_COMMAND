using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_COMMAND.PERSISTENCE.Outbox;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.DoctorServices;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
// using PostgreMigrateDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CommandConverts.DomainEvents;
using ClinicServiceDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ClinicServices.DomainEvents;
using ProcedureDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Procedures.DomainEvents;
using WorkingScheduleDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.WorkingSchedules.DomainEvents;
using ServicePromotionDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ServicePromotion.DomainEvents;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;
[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublishEndpoint publishEndpoint)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccurredOnUtc)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in messages)
        {
            var domainEvent = JsonConvert
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

                    case nameof(ClinicServiceDomainEvent.ClinicServiceCreated):
                        var clinicServiceCreated =
                            JsonConvert.DeserializeObject<ClinicServiceDomainEvent.ClinicServiceCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(clinicServiceCreated, context.CancellationToken);
                        break;
                    case nameof(ClinicServiceDomainEvent.ClinicServiceUpdated):
                        var clinicServiceUpdated =
                            JsonConvert.DeserializeObject<ClinicServiceDomainEvent.ClinicServiceUpdated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(clinicServiceUpdated, context.CancellationToken);
                        break;
                    case nameof(ClinicServiceDomainEvent.ClinicServiceDeleted):
                        var clinicServiceDeleted =
                            JsonConvert.DeserializeObject<ClinicServiceDomainEvent.ClinicServiceDeleted>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(clinicServiceDeleted, context.CancellationToken);
                        break;
                    case nameof(ProcedureDomainEvent.ProcedureCreated):
                        var procedureCreated =
                            JsonConvert.DeserializeObject<ProcedureDomainEvent.ProcedureCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(procedureCreated, context.CancellationToken);
                        break;
                    case nameof(ProcedureDomainEvent.ProcedureUpdate):
                        var procedureUpdated =
                            JsonConvert.DeserializeObject<ProcedureDomainEvent.ProcedureUpdate>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(procedureUpdated, context.CancellationToken);
                        break;
                    case nameof(ProcedureDomainEvent.ProcedureDelete):
                        var procedureDeleted =
                            JsonConvert.DeserializeObject<ProcedureDomainEvent.ProcedureDelete>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(procedureDeleted, context.CancellationToken);
                        break;
                    case nameof(WorkingScheduleDomainEvent.WorkingScheduleCreated):
                        var workingScheduleCreated =
                            JsonConvert.DeserializeObject<WorkingScheduleDomainEvent.WorkingScheduleCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(workingScheduleCreated, context.CancellationToken);
                        break;
                    case nameof(WorkingScheduleDomainEvent.WorkingScheduleDeleted):
                        var workingScheduleDeleted =
                            JsonConvert.DeserializeObject<WorkingScheduleDomainEvent.WorkingScheduleDeleted>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(workingScheduleDeleted, context.CancellationToken);
                        break;
                    case nameof(WorkingScheduleDomainEvent.WorkingScheduleUpdated):
                        var workingScheduleUpdated =
                            JsonConvert.DeserializeObject<WorkingScheduleDomainEvent.WorkingScheduleUpdated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(workingScheduleUpdated, context.CancellationToken);
                        break;
                    case nameof(ServicePromotionDomainEvent.ServicePromotionCreated):
                        var servicePromotionCreated =
                            JsonConvert.DeserializeObject<ServicePromotionDomainEvent.ServicePromotionCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(servicePromotionCreated, context.CancellationToken);
                        break;
                    case nameof(DomainEvents.DoctorServiceCreated):
                        var doctorServiceCreated =
                            JsonConvert.DeserializeObject<DomainEvents.DoctorServiceCreated>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(doctorServiceCreated, context.CancellationToken);
                        break;
                    case nameof(DomainEvents.DoctorServiceDeleted):
                        var doctorServiceDeleted =
                            JsonConvert.DeserializeObject<DomainEvents.DoctorServiceDeleted>(
                                outboxMessage.Content,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                });
                        await publishEndpoint.Publish(doctorServiceDeleted, context.CancellationToken);
                        break;

                    case nameof(BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CustomerSchedules.DomainEvents
                        .CustomerScheduleDeleted):
                        var customerScheduleDeleted =
                            JsonConvert
                                .DeserializeObject<BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.
                                    CustomerSchedules.DomainEvents.CustomerScheduleDeleted>(
                                    outboxMessage.Content,
                                    new JsonSerializerSettings
                                    {
                                        TypeNameHandling = TypeNameHandling.All
                                    });
                        await publishEndpoint.Publish(customerScheduleDeleted, context.CancellationToken);
                        break;
                    case nameof(BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CustomerSchedules.DomainEvents
                        .CustomerScheduleCreated):
                        var customerScheduleCreated =
                            JsonConvert
                                .DeserializeObject<BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.
                                    CustomerSchedules.DomainEvents.CustomerScheduleCreated>(
                                    outboxMessage.Content,
                                    new JsonSerializerSettings
                                    {
                                        TypeNameHandling = TypeNameHandling.All
                                    });
                        await publishEndpoint.Publish(customerScheduleCreated, context.CancellationToken);
                        break;

                    case nameof(BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CustomerSchedules.DomainEvents
                        .CustomerScheduleUpdateAfterPaymentCompleted):
                        var customerScheduleUpdateAfterPaymentCompleted =
                            JsonConvert
                                .DeserializeObject<BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.
                                    CustomerSchedules.DomainEvents.CustomerScheduleUpdateAfterPaymentCompleted>(
                                    outboxMessage.Content,
                                    new JsonSerializerSettings
                                    {
                                        TypeNameHandling = TypeNameHandling.All
                                    });
                        await publishEndpoint.Publish(customerScheduleUpdateAfterPaymentCompleted,
                            context.CancellationToken);
                        break;

                    case nameof(BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CustomerSchedules.DomainEvents
                        .CustomerScheduleUpdatedDoctorNote):
                        var customerScheduleUpdatedDoctorNote =
                            JsonConvert
                                .DeserializeObject<BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.
                                    CustomerSchedules.DomainEvents.CustomerScheduleUpdatedDoctorNote>(
                                    outboxMessage.Content,
                                    new JsonSerializerSettings
                                    {
                                        TypeNameHandling = TypeNameHandling.All
                                    });
                        await publishEndpoint.Publish(customerScheduleUpdatedDoctorNote, context.CancellationToken);
                        break;

                    case nameof(BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CustomerSchedules.DomainEvents
                        .CustomerScheduleUpdateDateAndTimeAndStatus):
                        var customerScheduleUpdateDateAndTime =
                            JsonConvert
                                .DeserializeObject<BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.
                                    CustomerSchedules.DomainEvents.CustomerScheduleUpdateDateAndTimeAndStatus>(
                                    outboxMessage.Content,
                                    new JsonSerializerSettings
                                    {
                                        TypeNameHandling = TypeNameHandling.All
                                    });

                        await publishEndpoint.Publish(customerScheduleUpdateDateAndTime, context.CancellationToken);
                        break;
                }

                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                outboxMessage.Error = ex.Message;
            }
        }

        await dbContext.SaveChangesAsync();
    }
}