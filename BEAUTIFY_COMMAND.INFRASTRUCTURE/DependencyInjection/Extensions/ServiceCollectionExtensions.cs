using System.Reflection;
using BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;
using BEAUTIFY_COMMAND.INFRASTRUCTURE.Locking;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.JsonConverters;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.Authentication;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.Caching;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.DependencyInjection.Extensions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.DependencyInjection.Options;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.Mail;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.Media;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.PasswordHasher;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.PipeObservers;
using CloudinaryDotNet;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using StackExchange.Redis;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddServicesInfrastructure(this IServiceCollection services)
    {
        services
            .AddTransient<IJwtTokenService, JwtTokenService>()
            .AddTransient<ICacheService, CacheService>()
            .AddSingleton<IMediaService, CloudinaryService>()
            .AddTransient<IPasswordHasherService, PasswordHasherService>()
            .AddSingleton<IMailService, MailService>()
            .AddSingleton<Cloudinary>(provider =>
            {
                var options = provider.GetRequiredService<IOptionsMonitor<CloudinaryOptions>>();
                return new Cloudinary(new Account(
                    options.CurrentValue.CloudName,
                    options.CurrentValue.ApiKey,
                    options.CurrentValue.ApiSecret));
            });
    }

    // Configure Redis
    public static void AddRedisInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");

        services.AddStackExchangeRedisCache(redisOptions =>
        {
            redisOptions.Configuration = connectionString;
        });

        // Register Redis ConnectionMultiplexer as singleton
        services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(connectionString ?? "localhost"));

        // Register the distributed lock service
        services.AddSingleton<IDistributedLockService, RedisDistributedLockService>();
    }

    // Configure for masstransit with rabbitMQ
    public static IServiceCollection AddMasstransitRabbitMQInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var masstransitConfiguration = new MasstransitConfiguration();
        configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

        var messageBusOption = new MessageBusOptions();
        configuration.GetSection(nameof(MessageBusOptions)).Bind(messageBusOption);

        services.AddMassTransit(cfg =>
        {
            // ===================== Setup for Consumer =====================
            cfg.AddConsumers(Assembly
                .GetExecutingAssembly()); // Add all of consumers to masstransit instead above command

            // ?? => Configure endpoint formatter. Not configure for producer Root Exchange
            cfg.SetKebabCaseEndpointNameFormatter(); // ??

            cfg.UsingRabbitMq((context, bus) =>
            {
                bus.Host(masstransitConfiguration.Host, masstransitConfiguration.Port, masstransitConfiguration.VHost,
                    h =>
                    {
                        h.Username(masstransitConfiguration.UserName);
                        h.Password(masstransitConfiguration.Password);
                    });

                bus.UseMessageRetry(retry
                    => retry.Incremental(
                        messageBusOption.RetryLimit,
                        messageBusOption.InitialInterval,
                        messageBusOption.IntervalIncrement));

                bus.UseNewtonsoftJsonSerializer();

                bus.ConfigureNewtonsoftJsonSerializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());
                    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                    return settings;
                });

                bus.ConfigureNewtonsoftJsonDeserializer(settings =>
                {
                    settings.Converters.Add(new TypeNameHandlingConverter(TypeNameHandling.Objects));
                    settings.Converters.Add(new DateOnlyJsonConverter());
                    settings.Converters.Add(new ExpirationDateOnlyJsonConverter());
                    return settings;
                });

                bus.ConnectReceiveObserver(new LoggingReceiveObserver());
                bus.ConnectConsumeObserver(new LoggingConsumeObserver());
                bus.ConnectPublishObserver(new LoggingPublishObserver());
                bus.ConnectSendObserver(new LoggingSendObserver());

                // Rename for Root Exchange and setup for consumer also
                bus.MessageTopology.SetEntityNameFormatter(new KebabCaseEntityNameFormatter());

                // ===================== Setup for Consumer =====================

                // Importantce to create Echange and Queue
                bus.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    // Configure Job
    public static void AddQuartzInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            // Process outbox messages job
            var outboxJobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure
                .AddJob<ProcessOutboxMessagesJob>(outboxJobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(outboxJobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithInterval(TimeSpan.FromMicroseconds(100))
                                        .RepeatForever()));

            // Customer schedule reminder job - runs every hour to handle all reminder types
            var reminderJobKey = new JobKey(nameof(CustomerScheduleReminderJob));
            configure
                .AddJob<CustomerScheduleReminderJob>(reminderJobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(reminderJobKey)
                            .WithCronSchedule("0 0 * * * ?", x => x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))));

            // Subscription purchase email job - runs every 5 minutes to send confirmation emails
            var subscriptionEmailJobKey = new JobKey(nameof(SubscriptionPurchaseEmailJob));
            configure
                .AddJob<SubscriptionPurchaseEmailJob>(subscriptionEmailJobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(subscriptionEmailJobKey)
                            .WithCronSchedule("0 */5 * * * ?", x => x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"))));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
    }

    public static OptionsBuilder<CloudinaryOptions> ConfigureCloudinaryOptionsInfrastucture(
        this IServiceCollection services, IConfigurationSection section)
    {
        return services
            .AddOptions<CloudinaryOptions>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    public static OptionsBuilder<MailOption> ConfigureMailOptionsInfrastucture(this IServiceCollection services,
        IConfigurationSection section)
    {
        return services
            .AddOptions<MailOption>()
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    // Configure MediatR
    public static void AddMediatRInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));
    }
}