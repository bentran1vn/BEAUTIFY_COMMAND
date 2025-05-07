using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.DOMAIN.MailTemplates;
public static class ClinicApplyEmailTemplate
{
    public static MailContent GetApplicationRegisteredTemplate(string email, string phoneNumber, string taxCode)
    {
        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Application Registered</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f5f5f5;
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
            background-color: #ffffff;
            border-radius: 0 0 8px 8px;
            border: 1px solid #e0e0e0;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }}
        .info-box {{
            background-color: #e8f5e9;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #4CAF50;
        }}
        .info-item {{
            margin: 10px 0;
            padding-left: 10px;
        }}
        .info-label {{
            font-weight: bold;
            color: #2E7D32;
        }}
        .thank-you {{
            margin-top: 20px;
            font-weight: bold;
            color: #4CAF50;
        }}
        .footer {{
            margin-top: 30px;
            text-align: center;
            font-size: 0.9em;
            color: #777;
            padding-top: 15px;
            border-top: 1px solid #ddd;
        }}
        @media (max-width: 600px) {{
            .header {{
                padding: 15px;
            }}
            .content {{
                padding: 15px;
            }}
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Application Received</h2>
    </div>
    <div class='content'>
        <p>Dear Clinic Administrator,</p>
        
        <p>Thank you for submitting your application to join our platform. We have successfully registered your information and your application is now being processed.</p>
        
        <div class='info-box'>
            <h3>Registration Information:</h3>
            <div class='info-item'>
                <span class='info-label'>Email:</span> {email}
            </div>
            <div class='info-item'>
                <span class='info-label'>Phone Number:</span> {phoneNumber}
            </div>
            <div class='info-item'>
                <span class='info-label'>Tax Code:</span> {taxCode}
            </div>
        </div>
        
        <p>Our team will review your application and get back to you as soon as possible. This process typically takes 1-3 business days.</p>
        
        <p class='thank-you'>Thank you for choosing our platform!</p>
        
        <p>If you have any questions, please don't hesitate to contact our support team.</p>
        
        <p>Best regards,<br>The Beautify Team</p>
    </div>
    
    <div class='footer'>
        <p>&copy; {DateTime.Now.Year} Beautify. All rights reserved.</p>
    </div>
</body>
</html>";

        return new MailContent
        {
            To = email,
            Subject = "Your Clinic Application Has Been Registered!",
            Body = body
        };
    }
}