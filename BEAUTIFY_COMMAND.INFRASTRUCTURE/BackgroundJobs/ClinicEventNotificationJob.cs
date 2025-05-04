using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.DOMAIN.MailTemplates;
using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;

[DisallowConcurrentExecution]
public class ClinicEventNotificationJob : IJob
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly ILogger<ClinicEventNotificationJob> _logger;
    private readonly IMailService _mailService;

    public ClinicEventNotificationJob(ApplicationDbContext applicationDbContext, ILogger<ClinicEventNotificationJob> logger, IMailService mailService)
    {
        _applicationDbContext = applicationDbContext;
        _logger = logger;
        _mailService = mailService;
    }
    
    private static readonly int[] ReminderHours = [12, 4, 0];

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation("Starting ClinicEventNotificationJob at {time}", DateTimeOffset.UtcNow);

            // Get Vietnam time zone
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var currentDateTimeVN = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);
            
            var events = await _applicationDbContext.Set<Event>()
                .Where(x => x.IsDeleted == false && 
                       x.Date.HasValue && 
                       x.Date.Value.ToDateTime(TimeOnly.MinValue) >= currentDateTimeVN.Date &&
                       x.Date.Value.ToDateTime(TimeOnly.MinValue) <= currentDateTimeVN.Date.AddDays(1) &&
                       x.StartDate.HasValue)
                .Include(x => x.Clinic)
                .ToListAsync();
                
            _logger.LogInformation("Found {count} upcoming events to check for notifications", events.Count);
                
            int notificationsSent = 0;
            
            foreach (var @event in events)
            {
                if (!@event.Date.HasValue || !@event.StartDate.HasValue || @event.Clinic == null)
                    continue;
                    
                // Create event datetime from Date and StartDate
                var eventDateTime = @event.Date.Value.ToDateTime(@event.StartDate.Value);
                
                // Convert to Vietnam time to match user expectations
                var eventTimeVN = TimeZoneInfo.ConvertTime(eventDateTime, vietnamTimeZone);
                var timeUntilEvent = eventTimeVN - currentDateTimeVN.DateTime;
                
                // Check if the event is at one of our reminder hours
                var hoursUntilEvent = (int)Math.Ceiling(timeUntilEvent.TotalHours);
                
                if (!ReminderHours.Contains(hoursUntilEvent))
                    continue;
                    
                // Get all followers of this clinic
                var followers = await _applicationDbContext.Set<Follower>()
                    .Where(f => f.ClinicId == @event.ClinicId && f.IsDeleted == false)
                    .Include(f => f.User)
                    .ToListAsync();
                    
                _logger.LogInformation("Event {eventId} ({eventName}) has {count} followers to notify at {hoursUntilEvent} hours until event", 
                    @event.Id, @event.Name, followers.Count, hoursUntilEvent);
                    
                foreach (var follower in followers)
                {
                    if (follower.User == null || string.IsNullOrEmpty(follower.User.Email))
                        continue;
                        
                    // Create email content using our template
                    var mailContent = ClinicEventReminderEmailTemplate.GetClinicEventReminderTemplate(
                        @event, 
                        @event.Clinic,
                        follower.User,
                        hoursUntilEvent);
                        
                    // Send the email
                    await _mailService.SendMail(mailContent);
                    notificationsSent++;
                }
            }
            
            _logger.LogInformation("Completed ClinicEventNotificationJob at {time}. Sent {count} notifications.", 
                DateTimeOffset.UtcNow, notificationsSent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing ClinicEventNotificationJob");
            throw; // Re-throw to let Quartz handle the exception
        }
    }
}