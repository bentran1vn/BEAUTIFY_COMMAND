using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.INFRASTRUCTURE.Mail;

namespace BEAUTIFY_COMMAND.CONTRACT.MailTemplates;

public static class SubscriptionPurchaseEmailTemplate
{
    public static MailContent GetSubscriptionPurchaseConfirmationTemplate(
        Clinic clinic, 
        SubscriptionPackage subscription,
        SystemTransaction transaction)
    {
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var purchaseDate = TimeZoneInfo.ConvertTime(transaction.TransactionDate, vietnamTimeZone);
        var expiryDate = purchaseDate.AddDays(subscription.Duration);
        
        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Subscription Purchase Confirmation</title>
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
            background-color: #4CAF50;
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
            background-color: #e8f5e9;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #4CAF50;
        }}
        .transaction-details {{
            background-color: #f5f5f5;
            padding: 15px;
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
        .button {{
            display: inline-block;
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin: 15px 0;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Subscription Purchase Confirmation</h2>
    </div>
    <div class='content'>
        <p>Dear {clinic.Name},</p>
        
        <p>Thank you for your purchase! Your subscription has been successfully activated.</p>
        
        <div class='subscription-details'>
            <h3>Subscription Details</h3>
            <p><strong>Package:</strong> {subscription.Name}</p>
            <p><strong>Description:</strong> {subscription.Description}</p>
            <p><strong>Duration:</strong> {subscription.Duration} days</p>
            <p><strong>Start Date:</strong> {purchaseDate:dd/MM/yyyy}</p>
            <p><strong>Expiry Date:</strong> {expiryDate:dd/MM/yyyy}</p>
            <p><strong>Branch Limit:</strong> {subscription.LimitBranch}</p>
            <p><strong>Livestream Limit:</strong> {subscription.LimitLiveStream}</p>
            <p><strong>Enhanced Viewer:</strong> {subscription.EnhancedViewer}</p>
        </div>
        
        <div class='transaction-details'>
            <h3>Transaction Details</h3>
            <p><strong>Transaction ID:</strong> {transaction.Id}</p>
            <p><strong>Amount:</strong> {transaction.Amount:N0} VND</p>
            <p><strong>Payment Method:</strong> {transaction.PaymentMethod ?? "Online Payment"}</p>
            <p><strong>Transaction Date:</strong> {purchaseDate:dd/MM/yyyy HH:mm}</p>
        </div>
        
        <p>You can now enjoy all the features included in your subscription package. If you have any questions or need assistance, please don't hesitate to contact our support team.</p>
        
        <p>Thank you for choosing our services!</p>
        
        <p>Best regards,</p>
        <p>The Beautify Team</p>
    </div>
    
    <div class='footer'>
        <p>&copy; {DateTime.Now.Year} Beautify. All rights reserved.</p>
    </div>
</body>
</html>
";

        return new MailContent
        {
            To = clinic.Email,
            Subject = $"Subscription Purchase Confirmation - {subscription.Name} Package",
            Body = body
        };
    }
}
