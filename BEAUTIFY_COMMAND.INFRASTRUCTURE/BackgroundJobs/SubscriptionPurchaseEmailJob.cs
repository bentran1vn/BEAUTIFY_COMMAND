using BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.PERSISTENCE;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.BackgroundJobs;
[DisallowConcurrentExecution]
public class SubscriptionPurchaseEmailJob(
    ApplicationDbContext dbContext,
    IMailService mailService,
    IRepositoryBase<SystemTransaction, Guid> systemTransactionRepository,
    ILogger<SubscriptionPurchaseEmailJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            logger.LogInformation("Starting SubscriptionPurchaseEmailJob at {time}", DateTimeOffset.UtcNow);

            // Get transactions that have been completed (status = 1) but haven't had emails sent yet
            // We'll use a custom property to track this, for now we'll check if the transaction has a SubscriptionPackageId
            // and is in status 1 (completed)
            var pendingTransactions = await dbContext.Set<SystemTransaction>()
                .Include(t => t.Clinic)
                .Include(t => t.SubscriptionPackage)
                .Where(t => t.Status == 1 &&
                            t.SubscriptionPackageId != null &&
                            !t.Clinic.IsDeleted &&
                            !t.SubscriptionPackage.IsDeleted)
                .ToListAsync(context.CancellationToken);

            logger.LogInformation("Found {count} pending subscription transactions to process",
                pendingTransactions.Count);

            foreach (var transaction in pendingTransactions)
                try
                {
                    // Skip if clinic email is missing
                    if (string.IsNullOrEmpty(transaction.Clinic?.Email))
                    {
                        logger.LogWarning("Clinic email is missing for transaction {transactionId}", transaction.Id);
                        continue;
                    }

                    // Create and send the confirmation email
                    var mailContent = SubscriptionPurchaseEmailTemplate.GetSubscriptionPurchaseConfirmationTemplate(
                        transaction.Clinic,
                        transaction.SubscriptionPackage,
                        transaction);

                    await mailService.SendMail(mailContent);

                    // Mark the transaction as having had an email sent
                    // For now, we'll update the status to 2 to indicate email sent
                    // In a real system, you might want to add a dedicated field for this
                    transaction.Status = 2; // 2 = Completed and email sent
                    systemTransactionRepository.Update(transaction);
                    await dbContext.SaveChangesAsync(context.CancellationToken); // Save changes to the database

                    logger.LogInformation(
                        "Sent subscription purchase confirmation email for transaction {transactionId} to {email}",
                        transaction.Id, transaction.Clinic.Email);
                }
                catch (Exception ex)
                {
                    // Log error but continue with other transactions
                    logger.LogError(ex,
                        "Error sending subscription purchase confirmation email for transaction {transactionId}",
                        transaction.Id);
                }

            logger.LogInformation("Completed SubscriptionPurchaseEmailJob at {time}", DateTimeOffset.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error executing SubscriptionPurchaseEmailJob");
            throw; // Re-throw to let Quartz handle the exception
        }
    }
}