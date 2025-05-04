using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;
using System;

namespace BEAUTIFY_COMMAND.DOMAIN.MailTemplates;

public static class ClinicEventReminderEmailTemplate
{
    public static MailContent GetClinicEventReminderTemplate(
        Event @event,
        Clinic clinic,
        User user,
        int hoursRemaining)
    {
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var eventDateTime = @event.Date!.Value.ToDateTime(@event.StartDate!.Value);
        var localEventDateTime = TimeZoneInfo.ConvertTime(eventDateTime, vietnamTimeZone);

        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Event Reminder</title>
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
            background-color: #4a90e2;
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
        .event-details {{
            background-color: #e3f2fd;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #4a90e2;
        }}
        .clinic-details {{
            background-color: #e8f5e9;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #4caf50;
        }}
        .start-soon {{
            background-color: #fff8e1;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #ffc107;
        }}
        .action-button {{
            display: inline-block;
            background-color: #4a90e2;
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin: 15px 0;
        }}
        .livestream-badge {{
            display: inline-block;
            background-color: #e91e63;
            color: white;
            padding: 5px 10px;
            border-radius: 3px;
            font-size: 0.9em;
            margin: 5px 0;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
            padding-top: 15px;
            border-top: 1px solid #eee;
        }}
        img.event-image {{
            max-width: 100%;
            height: auto;
            border-radius: 5px;
            margin: 10px 0;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Event Reminder</h2>
    </div>
    <div class='content'>
        <p>Dear {user.FirstName} {user.LastName},</p>
        
        <div class='start-soon'>
            {(hoursRemaining == 0 ? 
                "<h3>🎉 The event is starting now!</h3>" : 
                $"<h3>⏰ Event starts in {hoursRemaining} hour{(hoursRemaining != 1 ? "s" : "")}!</h3>")}
            <p>This is a reminder about an upcoming event from a clinic you're following.</p>
        </div>
        
        <div class='event-details'>
            <h3>{@event.Name}</h3>
            {(!string.IsNullOrEmpty(@event.Image) ? $"<img src='{@event.Image}' alt='Event Image' class='event-image'>" : "")}
            {(!string.IsNullOrEmpty(@event.Description) ? $"<p>{@event.Description}</p>" : "")}
            <p><strong>Date:</strong> {localEventDateTime:dddd, MMMM d, yyyy}</p>
            <p><strong>Time:</strong> {localEventDateTime:h:mm tt}{(@event.EndDate.HasValue ? $" - {@event.EndDate.Value:h:mm tt}" : "")}</p>
            {(@event.LivestreamRoomId.HasValue ? "<p><span class='livestream-badge'>LIVE</span> This event includes a livestream!</p>" : "")}
        </div>
        
        <div class='clinic-details'>
            <h3>Hosted by: {clinic.Name}</h3>
            {(!string.IsNullOrEmpty(clinic.Address) ? $"<p><strong>Address:</strong> {clinic.Address}</p>" : "")}
            {(!string.IsNullOrEmpty(clinic.PhoneNumber) ? $"<p><strong>Contact:</strong> {clinic.PhoneNumber}</p>" : "")}
        </div>
        
        <a href='https://beautify.asia/event/{@event.Id}' class='action-button'>View Event Details</a>
        
        {(@event.LivestreamRoomId.HasValue ? 
            $"<a href='https://beautify.asia/livestream/{@event.LivestreamRoomId}' class='action-button' style='background-color: #e91e63;'>Join Livestream</a>" : "")}
        
        <p>We hope to see you there!</p>
        
        <p>Best regards,</p>
        <p>The Beautify Team</p>
    </div>
    
    <div class='footer'>
        <p>&copy; {DateTime.Now.Year} Beautify. All rights reserved.</p>
        <p>You received this email because you follow {clinic.Name} on Beautify. 
           <a href='https://beautify.asia/profile/settings/notifications'>Manage your notification settings</a>.</p>
    </div>
</body>
</html>";

        // Customize the subject based on how soon the event will start
        var subject = hoursRemaining switch
        {
            0 => $"🎉 STARTING NOW: {@event.Name} by {clinic.Name}",
            <= 4 => $"⏰ COMING SOON: {@event.Name} starts in {hoursRemaining} hour{(hoursRemaining != 1 ? "s" : "")}",
            <= 12 => $"Reminder: {@event.Name} starts in {hoursRemaining} hours",
            _ => $"Upcoming Event: {@event.Name} by {clinic.Name}"
        };

        return new MailContent
        {
            To = user.Email,
            Subject = subject,
            Body = body
        };
    }
}