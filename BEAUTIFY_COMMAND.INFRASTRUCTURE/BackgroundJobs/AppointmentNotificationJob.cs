using BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Abstractions.Repositories;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;
[DisallowConcurrentExecution]
public class AppointmentNotificationJob(
    ApplicationDbContext dbContext,
    IMailService mailService,
    IRepositoryBase<CustomerScheduleReminder, Guid> reminderRepository,
    ILogger<AppointmentNotificationJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            logger.LogInformation("Starting AppointmentNotificationJob at {time}", DateTimeOffset.UtcNow);

            // Get Vietnam time zone
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var currentDateTimeVN = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);
            var currentDateVN = DateOnly.FromDateTime(currentDateTimeVN.DateTime);

            // Process each reminder type
            await ProcessSevenDayReminders(currentDateVN, context.CancellationToken);
            await ProcessThreeDayReminders(currentDateVN, context.CancellationToken);
            await ProcessOneDayReminders(currentDateVN, context.CancellationToken);
            await ProcessTwoHourReminders(currentDateTimeVN, context.CancellationToken);
            await ProcessMissedAppointments(currentDateTimeVN, context.CancellationToken);

            logger.LogInformation("Completed AppointmentNotificationJob at {time}", DateTimeOffset.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing AppointmentNotificationJob");
            throw; // Re-throw to let Quartz handle the exception
        }
    }

    private async Task ProcessSevenDayReminders(DateOnly currentDateVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules for 7 days from now
            var targetDate = currentDateVN.AddDays(7);
            logger.LogInformation("Looking for schedules on {date} for 7-day reminders", targetDate);

            await ProcessRemindersForDate(targetDate, CustomerScheduleReminder.ReminderTypes.SevenDay,
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing 7-day reminders");
        }
    }

    private async Task ProcessThreeDayReminders(DateOnly currentDateVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules for 3 days from now
            var targetDate = currentDateVN.AddDays(3);
            logger.LogInformation("Looking for schedules on {date} for 3-day reminders", targetDate);

            await ProcessRemindersForDate(targetDate, CustomerScheduleReminder.ReminderTypes.ThreeDay,
                cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing 3-day reminders");
        }
    }

    private async Task ProcessOneDayReminders(DateOnly currentDateVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules for tomorrow
            var targetDate = currentDateVN.AddDays(1);
            logger.LogInformation("Looking for schedules on {date} for 1-day reminders", targetDate);

            await ProcessRemindersForDate(targetDate, CustomerScheduleReminder.ReminderTypes.OneDay, cancellationToken);
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

            logger.LogInformation(
                "Looking for schedules on {date} between {startTime} and {endTime} for 2-hour reminders",
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

            logger.LogInformation("Found {count} upcoming schedules that need 2-hour reminders",
                upcomingSchedules.Count);

            foreach (var schedule in upcomingSchedules)
                await SendReminderEmail(schedule, CustomerScheduleReminder.ReminderTypes.TwoHour, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing 2-hour reminders");
        }
    }

    private async Task ProcessMissedAppointments(DateTimeOffset currentDateTimeVN, CancellationToken cancellationToken)
    {
        try
        {
            // Get schedules that have passed but still have a pending status
            var currentDate = DateOnly.FromDateTime(currentDateTimeVN.DateTime);
            var currentTime = currentDateTimeVN.TimeOfDay;

            logger.LogInformation("Looking for missed appointments up to {date} {time}", currentDate, currentTime);

            // Get IDs of schedules that already have missed appointment notifications
            var notifiedScheduleIds = await dbContext.Set<CustomerScheduleReminder>()
                .Where(r => r.ReminderType == CustomerScheduleReminder.ReminderTypes.MissedAppointment)
                .Select(r => r.CustomerScheduleId)
                .ToListAsync(cancellationToken);

            // Find schedules that:
            // 1. Are for today or in the past
            // 2. Have a start time that has already passed
            // 3. Still have a pending status
            // 4. Haven't already been notified
            var missedSchedules = await dbContext.Set<CustomerSchedule>()
                .Include(cs => cs.Customer)
                .Include(cs => cs.Service)
                .Include(cs => cs.Doctor)
                .Include(cs => cs.Doctor.User)
                .Include(cs => cs.Doctor.Clinic)
                .Where(cs =>
                    // Date is today or in the past
                    (cs.Date < currentDate ||
                     (cs.Date == currentDate && cs.StartTime < currentTime)) &&
                    // Status is still pending
                    (cs.Status == Constant.OrderStatus.ORDER_PENDING ||
                     cs.Status == Constant.OrderStatus.ORDER_WAITING_APPROVAL) &&
                    // Customer has an email
                    cs.Customer.Email != null &&
                    // No notification has been sent yet
                    !notifiedScheduleIds.Contains(cs.Id))
                .ToListAsync(cancellationToken);

            logger.LogInformation("Found {count} missed appointments to notify", missedSchedules.Count);

            foreach (var schedule in missedSchedules)
                await SendReminderEmail(schedule, CustomerScheduleReminder.ReminderTypes.MissedAppointment,
                    cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing missed appointment notifications");
        }
    }

    private async Task ProcessRemindersForDate(DateOnly targetDate, string reminderType,
        CancellationToken cancellationToken)
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

        foreach (var schedule in upcomingSchedules) await SendReminderEmail(schedule, reminderType, cancellationToken);
    }

    private async Task SendReminderEmail(CustomerSchedule schedule, string reminderType,
        CancellationToken cancellationToken)
    {
        try
        {
            // Use the template to create the email content
            var mailContent = AppointmentNotificationEmailTemplate.GetEmailTemplate(schedule, reminderType);

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

            logger.LogInformation("Sent {reminderType} notification email for schedule {scheduleId} to {email}",
                reminderType, schedule.Id, schedule.Customer.Email);
        }
        catch (Exception ex)
        {
            // Log error but continue with other reminders
            logger.LogError(ex, "Error sending {reminderType} notification for schedule {scheduleId}",
                reminderType, schedule.Id);
        }
    }
}