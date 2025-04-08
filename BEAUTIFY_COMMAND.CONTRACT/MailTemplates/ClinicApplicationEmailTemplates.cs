namespace BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
public static class ClinicApplicationEmailTemplates
{
    public static string GetApprovedTemplate(string email, string password)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .header {{
            background-color: #4CAF50;
            color: white;
            padding: 15px;
            text-align: center;
            border-radius: 5px 5px 0 0;
        }}
        .content {{
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 0 0 5px 5px;
            border: 1px solid #ddd;
        }}
        .credentials {{
            background-color: #e9f7ef;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
        }}
        .button {{
            display: inline-block;
            padding: 10px 20px;
            background-color: #4CAF50;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            margin: 10px 0;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Clinic Application Approved</h2>
    </div>
    <div class='content'>
        <p>Dear {email},</p>
        <p>First of all, our System thanks you for submitting your application!</p>
        <p>We are pleased to inform you that your application has been approved.</p>
        
        <div class='credentials'>
            <p><strong>Your admin account credentials:</strong></p>
            <p>Email: {email}</p>
            <p>Password: {password}</p>
        </div>
        
        <p>You can now log in to our system and start managing your clinic.</p>
        <p>If you have any questions, please reply to this email.</p>
        <div class='footer'>
            <p>Thank you for choosing our platform!</p>
        </div>
    </div>
</body>
</html>";
    }

    public static string GetRejectedTemplate(string email, string reason)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .header {{
            background-color: #f39c12;
            color: white;
            padding: 15px;
            text-align: center;
            border-radius: 5px 5px 0 0;
        }}
        .content {{
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 0 0 5px 5px;
            border: 1px solid #ddd;
        }}
        .reason {{
            background-color: #fdebd0;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Clinic Application Update</h2>
    </div>
    <div class='content'>
        <p>Dear {email},</p>
        <p>First of all, our System thanks you for submitting your application!</p>
        
        <div class='reason'>
            <p>We regret to inform you that your application does not meet our requirements:</p>
            <p><strong>Reason:</strong> {reason}</p>
        </div>
        
        <p>You may prepare and submit a new application in the future.</p>
        <p>If you have any questions, please reply to this email.</p>
        <div class='footer'>
            <p>Thank you for your interest in our platform.</p>
        </div>
    </div>
</body>
</html>";
    }

    public static string GetBannedTemplate(string email, string reason)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            line-height: 1.6;
            color: #333;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
        }}
        .header {{
            background-color: #e74c3c;
            color: white;
            padding: 15px;
            text-align: center;
            border-radius: 5px 5px 0 0;
        }}
        .content {{
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 0 0 5px 5px;
            border: 1px solid #ddd;
        }}
        .reason {{
            background-color: #fadbd8;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Clinic Application Decision</h2>
    </div>
    <div class='content'>
        <p>Dear {email},</p>
        <p>First of all, our System thanks you for submitting your application!</p>
        
        <div class='reason'>
            <p>We are sorry to inform you that your application has been banned due to violation of our standards:</p>
            <p><strong>Reason:</strong> {reason}</p>
        </div>
        
        <p>This decision is final and you will not be able to submit new applications.</p>
        <p>If you believe this is an error, please reply to this email.</p>
        <div class='footer'>
            <p>Thank you for your understanding.</p>
        </div>
    </div>
</body>
</html>";
    }
}