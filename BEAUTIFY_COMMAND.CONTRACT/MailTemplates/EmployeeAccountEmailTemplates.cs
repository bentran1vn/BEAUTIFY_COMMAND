using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
public static class EmployeeAccountEmailTemplates
{
    public static string GetAccountCreationTemplate(string email, string firstName, string password, string roleType)
    {
        var roleDisplayName = roleType == Constant.Role.DOCTOR ? "Doctor" : "Clinic Staff";

        return $@"
<!DOCTYPE html>
<html>
<head>
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
            background-color: #4285F4;
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
        .credentials-box {{
            background-color: #ffffff;
            padding: 20px;
            border-radius: 6px;
            margin: 20px 0;
            box-shadow: 0 2px 4px rgba(0,0,0,0.05);
            border-left: 4px solid #4285F4;
        }}
        .footer {{
            margin-top: 25px;
            font-size: 0.9em;
            color: #666;
            text-align: center;
            border-top: 1px solid #eee;
            padding-top: 15px;
        }}
        .password-warning {{
            color: #d32f2f;
            font-weight: bold;
            background-color: #ffebee;
            padding: 10px;
            border-radius: 4px;
            margin: 15px 0;
        }}
        .role-badge {{
            display: inline-block;
            padding: 3px 8px;
            background-color: #e8f0fe;
            color: #4285F4;
            border-radius: 12px;
            font-size: 0.8em;
            font-weight: bold;
            margin-left: 5px;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Your {roleDisplayName} Account Has Been Created</h2>
    </div>
    <div class='content'>
        <p>Dear {firstName},</p>
        <p>Your employee account has been successfully created in our clinic management system.</p>
        
        <div class='credentials-box'>
            <p><strong>Account Details:</strong></p>
            <p>Email: {email}</p>
            <p>Temporary Password: {password}</p>
            <p>Role: {roleDisplayName} <span class='role-badge'>{(roleType == Constant.Role.DOCTOR ? "DR" : "STAFF")}</span></p>
        </div>
        
        <div class='password-warning'>
            <p>⚠️ For security reasons, please change your password immediately after first login.</p>
        </div>
        
        <p>You can now access the system using the credentials above.</p>
        <p>If you didn't request this account or need any assistance, please contact your clinic administrator.</p>
        
        <div class='footer'>
            <p>© {DateTime.Now.Year} Clinic Management System. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";
    }
}