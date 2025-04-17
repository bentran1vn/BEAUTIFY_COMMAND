using BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;
[DisallowConcurrentExecution]
public class SubscriptionExpiryReminderJob(
    ApplicationDbContext dbContext,
    IMailService mailService,
    ILogger<SubscriptionExpiryReminderJob> logger) : IJob
{
    // Define reminder thresholds in days
    private static readonly int[] ReminderDays = { 30, 14, 7, 3, 1 };

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            logger.LogInformation("Starting SubscriptionExpiryReminderJob at {time}", DateTimeOffset.UtcNow);

            // Get Vietnam time zone
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var currentDateTimeVN = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, vietnamTimeZone);

            // Get all completed subscription transactions (status = 2 means completed and email sent)
            var completedTransactions = await dbContext.Set<SystemTransaction>()
                .Include(t => t.Clinic)
                .Include(t => t.SubscriptionPackage)
                .Where(t => t.Status == 2 &&
                            t.SubscriptionPackageId != null &&
                            !t.Clinic.IsDeleted &&
                            !t.SubscriptionPackage.IsDeleted)
                .ToListAsync(context.CancellationToken);

            logger.LogInformation("Found {count} completed subscription transactions to check for expiry",
                completedTransactions.Count);

            foreach (var transaction in completedTransactions)
                try
                {
                    // Skip if clinic email is missing
                    if (string.IsNullOrEmpty(transaction.Clinic?.Email))
                    {
                        logger.LogWarning("Clinic email is missing for transaction {transactionId}", transaction.Id);
                        continue;
                    }

                    // Calculate expiry date based on transaction date and subscription duration
                    var expiryDate = transaction.TransactionDate.AddDays(transaction.SubscriptionPackage.Duration);

                    // Calculate days remaining until expiry
                    var daysRemaining = (expiryDate - currentDateTimeVN).Days;

                    // Check if we need to send a reminder for this subscription
                    if (ReminderDays.Contains(daysRemaining))
                    {
                        // Create and send the reminder email
                        var mailContent = SubscriptionExpiryReminderEmailTemplate.GetSubscriptionExpiryReminderTemplate(
                            transaction.Clinic,
                            transaction.SubscriptionPackage,
                            transaction,
                            expiryDate,
                            daysRemaining);

                        await mailService.SendMail(mailContent);

                        logger.LogInformation(
                            "Sent subscription expiry reminder email for transaction {transactionId} to {email}. Expires in {days} days on {expiryDate}",
                            transaction.Id, transaction.Clinic.Email, daysRemaining, expiryDate);
                    }
                }
                catch (Exception ex)
                {
                    // Log the error but continue processing other transactions
                    logger.LogError(ex, "Error processing subscription expiry reminder for transaction {transactionId}",
                        transaction.Id);
                }

            logger.LogInformation("Completed SubscriptionExpiryReminderJob at {time}", DateTimeOffset.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing SubscriptionExpiryReminderJob");
            throw; // Re-throw to let Quartz handle the exception
        }
    }
}