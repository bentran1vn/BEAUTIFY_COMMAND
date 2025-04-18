using System.Globalization;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
public static class BookingEmailTemplate
{
    public static MailContent GetBookingConfirmationTemplate(
        User user,
        Order order,
        CustomerSchedule customerSchedule,
        Staff doctor,
        Clinic clinic,
        Service service,
        decimal depositAmount,
        DateOnly bookingDate,
        TimeSpan startTime)
    {
        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Booking Confirmation</title>
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
            background: linear-gradient(135deg, #f8bbd0, #ec407a);
            color: white;
            padding: 25px;
            text-align: center;
            border-radius: 8px 8px 0 0;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }}
        .content {{
            padding: 30px;
            background-color: white;
            border-radius: 0 0 8px 8px;
            border: 1px solid #e0e0e0;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }}
        .booking-details {{
            background-color: #fce4ec;
            padding: 20px;
            border-radius: 8px;
            margin: 20px 0;
            border-left: 4px solid #ec407a;
        }}
        .booking-details h3 {{
            color: #c2185b;
            margin-top: 0;
        }}
        .booking-details ul {{
            list-style-type: none;
            padding: 0;
        }}
        .booking-details li {{
            padding: 8px 0;
            border-bottom: 1px solid #f8bbd0;
        }}
        .booking-details li:last-child {{
            border-bottom: none;
        }}
        .deposit-info {{
            background-color: #e8f5e9;
            padding: 15px;
            border-radius: 8px;
            margin: 20px 0;
            border-left: 4px solid #4CAF50;
        }}
        .important-note {{
            background-color: #fff8e1;
            padding: 15px;
            border-radius: 8px;
            margin: 20px 0;
            border-left: 4px solid #ffc107;
        }}
        .footer {{
            margin-top: 30px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
            padding-top: 20px;
            border-top: 1px solid #eee;
        }}
        .clinic-name {{
            font-weight: bold;
            color: #c2185b;
        }}
        @media only screen and (max-width: 480px) {{
            body {{
                padding: 10px;
            }}
            .header, .content {{
                padding: 15px;
            }}
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Booking Confirmation</h2>
    </div>
    <div class='content'>
        <p>Dear {user.FirstName} {user.LastName},</p>

        <p>Your booking has been successfully created. Here are the details:</p>

        <div class='booking-details'>
            <h3>Booking Information</h3>
            <ul>
                <li><strong>Booking ID:</strong> {order.Id}</li>
                <li><strong>Booking Date:</strong> {bookingDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}</li>
                <li><strong>Start Time:</strong> {startTime.ToString(@"hh\:mm")}</li>
                <li><strong>End Time:</strong> {customerSchedule.EndTime?.ToString(@"hh\:mm") ?? "N/A"}</li>
                <li><strong>Service:</strong> {service.Name}</li>
                <li><strong>Doctor:</strong> {doctor.FirstName} {doctor.LastName}</li>
                <li><strong>Address:</strong> {clinic.Address}</li>
            </ul>
        </div>

        <div class='deposit-info'>
            <p>A deposit of <strong>{depositAmount}</strong> has been deducted from your wallet balance.</p>
        </div>

        <div class='important-note'>
            <p><strong>Important:</strong> When arriving at the clinic, please provide this email or your full name and phone number to the staff.</p>
        </div>

        <p>Thank you for choosing our service!</p>

        <p>Best regards,</p>
        <p class='clinic-name'>{clinic.Name} Clinic</p>
    </div>

    <div class='footer'>
        <p>&copy; {DateTime.Now.Year} {clinic.Name}. All rights reserved.</p>
    </div>
</body>
</html>
";

        return new MailContent
        {
            To = user.Email,
            Subject =
                $"Booking Confirmation - {clinic.Name} - {bookingDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}",
            Body = body
        };
    }
}