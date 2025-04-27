using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.DOMAIN.MailTemplates;
public static class SubscriptionExpiryReminderEmailTemplate
{
    public static MailContent GetSubscriptionExpiryReminderTemplate(
        Clinic clinic,
        SubscriptionPackage subscription,
        SystemTransaction transaction,
        DateTimeOffset expiryDate,
        int daysRemaining)
    {
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var localExpiryDate = TimeZoneInfo.ConvertTime(expiryDate, vietnamTimeZone);

        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Subscription Expiry Reminder</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .header {{
            background-color: #ff9800;
            color: white;
            padding: 20px;
            text-align: center;
            border-radius: 8px 8px 0 0;
        }}
        .content {{
            padding: 25px;
            background-color: #f9f9f9;
            border-radius: 0 0 8px 8px;
            border: 1px solid #e0e0e0;
        }}
        .subscription-details {{
            background-color: #fff3e0;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #ff9800;
        }}
        .expiry-warning {{
            background-color: #ffebee;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #f44336;
        }}
        .action-button {{
            display: inline-block;
            background-color: #ff9800;
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin: 15px 0;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
            padding-top: 15px;
            border-top: 1px solid #eee;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Subscription Expiry Reminder</h2>
    </div>
    <div class='content'>
        <p>Dear {clinic.Name},</p>
        
        <div class='expiry-warning'>
            <h3>⚠️ Your subscription is about to expire!</h3>
            <p>Your <strong>{subscription.Name}</strong> subscription package will expire in <strong>{daysRemaining} days</strong> on <strong>{localExpiryDate:dd/MM/yyyy}</strong>.</p>
        </div>
        
        <div class='subscription-details'>
            <h3>Subscription Details</h3>
            <p><strong>Package:</strong> {subscription.Name}</p>
            <p><strong>Description:</strong> {subscription.Description}</p>
            <p><strong>Expiry Date:</strong> {localExpiryDate:dd/MM/yyyy}</p>
            <p><strong>Branch Limit:</strong> {subscription.LimitBranch}</p>
            <p><strong>Livestream Limit:</strong> {subscription.LimitLiveStream}</p>
            <p><strong>Enhanced Viewer:</strong> {subscription.EnhancedViewer}</p>
        </div>
        
        <p>To ensure uninterrupted access to all features and services, please renew your subscription before the expiry date.</p>
        
        <p>If your subscription expires:</p>
        <ul>
            <li>You may lose access to premium features</li>
            <li>Your ability to manage multiple branches may be limited</li>
            <li>Livestream capabilities may be restricted</li>
        </ul>
        
       <a href='https://beautify.asia/clinicManager/buy-package' class='action-button'>Renew Subscription Now</a>
        
        <p>If you have any questions or need assistance with the renewal process, please don't hesitate to contact our support team.</p>
        
        <p>Thank you for choosing our services!</p>
        
        <p>Best regards,</p>
        
        <p>The Beautify Team</p>
    </div>
    
    <div class='footer'>
        <p>&copy; {DateTime.Now.Year} Beautify. All rights reserved.</p>
    </div>
</body>
</html>";

        // Customize the subject based on how soon the subscription will expire
        var subject = daysRemaining switch
        {
            <= 3 => $"URGENT: Your {subscription.Name} subscription expires in {daysRemaining} days!",
            <= 7 => $"Important: Your {subscription.Name} subscription expires in {daysRemaining} days",
            _ => $"Reminder: Your {subscription.Name} subscription will expire soon"
        };

        return new MailContent
        {
            To = clinic.Email,
            Subject = subject,
            Body = body
        };
    }
}