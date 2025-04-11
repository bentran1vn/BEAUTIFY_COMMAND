using BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Abstractions.Repositories;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;

[DisallowConcurrentExecution]
public class CustomerScheduleReminderJob(
    ApplicationDbContext dbContext,
    IMailService mailService,
    IRepositoryBase<CustomerScheduleReminder, Guid> reminderRepository,
    ILogger<CustomerScheduleReminderJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            logger.LogInformation("Starting CustomerScheduleReminderJob at {time}", DateTimeOffset.UtcNow);

            // Get Vietnam time zone
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var currentDateTimeVN = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);
            var currentDateVN = DateOnly.FromDateTime(currentDateTimeVN.DateTime);

            // Process each reminder type
            await ProcessSevenDayReminders(currentDateTimeVN, currentDateVN, context.CancellationToken);
            await ProcessThreeDayReminders(currentDateTimeVN, currentDateVN, context.CancellationToken);
            await ProcessOneDayReminders(currentDateTimeVN, currentDateVN, context.CancellationToken);
            await ProcessTwoHourReminders(currentDateTimeVN, context.CancellationToken);

            logger.LogInformation("Completed CustomerScheduleReminderJob at {time}", DateTimeOffset.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing CustomerScheduleReminderJob");
            throw; // Re-throw to let Quartz handle the exception
        }
    }

    private async Task ProcessSevenDayReminders(DateTimeOffset currentDateTimeVN, DateOnly currentDateVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules for 7 days from now
            var targetDate = currentDateVN.AddDays(7);
            logger.LogInformation("Looking for schedules on {date} for 7-day reminders", targetDate);

            await ProcessRemindersForDate(targetDate, CustomerScheduleReminder.ReminderTypes.SevenDay, "7 ngày", cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing 7-day reminders");
        }
    }

    private async Task ProcessThreeDayReminders(DateTimeOffset currentDateTimeVN, DateOnly currentDateVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules for 3 days from now
            var targetDate = currentDateVN.AddDays(3);
            logger.LogInformation("Looking for schedules on {date} for 3-day reminders", targetDate);

            await ProcessRemindersForDate(targetDate, CustomerScheduleReminder.ReminderTypes.ThreeDay, "3 ngày", cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing 3-day reminders");
        }
    }

    private async Task ProcessOneDayReminders(DateTimeOffset currentDateTimeVN, DateOnly currentDateVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules for tomorrow
            var targetDate = currentDateVN.AddDays(1);
            logger.LogInformation("Looking for schedules on {date} for 1-day reminders", targetDate);

            await ProcessRemindersForDate(targetDate, CustomerScheduleReminder.ReminderTypes.OneDay, "1 ngày", cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing 1-day reminders");
        }
    }

    private async Task ProcessTwoHourReminders(DateTimeOffset currentDateTimeVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules for the current day that are 2 hours from now
            var currentDate = DateOnly.FromDateTime(currentDateTimeVN.DateTime);
            var currentTime = currentDateTimeVN.TimeOfDay;
            var twoHoursLater = currentTime.Add(TimeSpan.FromHours(2));
            var threeHoursLater = currentTime.Add(TimeSpan.FromHours(3));

            logger.LogInformation("Looking for schedules on {date} between {startTime} and {endTime} for 2-hour reminders",
                currentDate, twoHoursLater, threeHoursLater);

            // Get IDs of schedules that already have 2-hour reminders
            var schedulesWithReminders = await dbContext.Set<CustomerScheduleReminder>()
                .Where(r => r.ReminderType == CustomerScheduleReminder.ReminderTypes.TwoHour)
                .Select(r => r.CustomerScheduleId)
                .ToListAsync(cancellationToken);

            // Find schedules that start between 2 and 3 hours from now and don't already have reminders
            var upcomingSchedules = await dbContext.Set<CustomerSchedule>()
                .Include(cs => cs.Customer)
                .Include(cs => cs.Service)
                .Include(cs => cs.Doctor)
                .Include(cs => cs.Doctor.User)
                .Include(cs => cs.Doctor.Clinic)
                .Where(cs => cs.Date == currentDate &&
                             cs.StartTime >= twoHoursLater &&
                             cs.StartTime <= threeHoursLater &&
                             (cs.Status == Constant.OrderStatus.ORDER_PENDING ||
                              cs.Status == Constant.OrderStatus.ORDER_WAITING_APPROVAL) &&
                             !schedulesWithReminders.Contains(cs.Id) &&
                             cs.Customer.Email != null) // Only include schedules with valid customer emails
                .ToListAsync(cancellationToken);

            logger.LogInformation("Found {count} upcoming schedules that need 2-hour reminders", upcomingSchedules.Count);

            foreach (var schedule in upcomingSchedules)
            {
                await SendReminderEmail(schedule, CustomerScheduleReminder.ReminderTypes.TwoHour, "2 giờ", cancellationToken);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing 2-hour reminders");
        }
    }

    private async Task ProcessRemindersForDate(DateOnly targetDate, string reminderType, string reminderTimeText, CancellationToken cancellationToken)
    {
        // Get IDs of schedules that already have reminders of this type
        var schedulesWithReminders = await dbContext.Set<CustomerScheduleReminder>()
            .Where(r => r.ReminderType == reminderType)
            .Select(r => r.CustomerScheduleId)
            .ToListAsync(cancellationToken);

        // Query upcoming schedules for the target date that don't already have reminders
        var upcomingSchedules = await dbContext.Set<CustomerSchedule>()
            .Include(cs => cs.Customer)
            .Include(cs => cs.Service)
            .Include(cs => cs.Doctor)
            .Include(cs => cs.Doctor.User)
            .Include(cs => cs.Doctor.Clinic)
            .Where(cs => cs.Date == targetDate &&
                         (cs.Status == Constant.OrderStatus.ORDER_PENDING ||
                          cs.Status == Constant.OrderStatus.ORDER_WAITING_APPROVAL) &&
                         !schedulesWithReminders.Contains(cs.Id) &&
                         cs.Customer.Email != null) // Only include schedules with valid customer emails
            .ToListAsync(cancellationToken);

        logger.LogInformation("Found {count} upcoming schedules that need {reminderType} reminders",
            upcomingSchedules.Count, reminderType);

        foreach (var schedule in upcomingSchedules)
        {
            await SendReminderEmail(schedule, reminderType, reminderTimeText, cancellationToken);
        }
    }

    private async Task SendReminderEmail(CustomerSchedule schedule, string reminderType, string reminderTimeText, CancellationToken cancellationToken)
    {
        try
        {
            // Use the template to create the email content
            var mailContent = ScheduleReminderEmailTemplate.GetScheduleReminderTemplate(schedule, reminderTimeText);

            // Send the email
            await mailService.SendMail(mailContent);

            // Record that this reminder has been sent
            var reminder = new CustomerScheduleReminder
            {
                Id = Guid.NewGuid(),
                CustomerScheduleId = schedule.Id,
                ReminderType = reminderType,
                SentOnUtc = DateTimeOffset.UtcNow
            };

            reminderRepository.Add(reminder);
            await dbContext.SaveChangesAsync(cancellationToken); // Save changes to the database

            logger.LogInformation("Sent {reminderType} reminder email for schedule {scheduleId} to {email}",
                reminderType, schedule.Id, schedule.Customer.Email);
        }
        catch (Exception ex)
        {
            // Log error but continue with other reminders
            logger.LogError(ex, "Error sending {reminderType} reminder for schedule {scheduleId}",
                reminderType, schedule.Id);
        }
    }
}
