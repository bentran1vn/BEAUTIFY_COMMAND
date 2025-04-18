using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.APPLICATION.Abstractions;

namespace BEAUTIFY_COMMAND.CONTRACT.MailTemplates;
public static class AppointmentNotificationEmailTemplate
{
    public static MailContent GetEmailTemplate(CustomerSchedule schedule, string reminderType)
    {
        // Common data extraction
        var customerName = $"{schedule.Customer?.FirstName} {schedule.Customer?.LastName}";
        var serviceName = schedule.Service?.Name ?? "Dịch vụ";
        var doctorName = $"{schedule.Doctor?.User?.FirstName} {schedule.Doctor?.User?.LastName}";
        var clinicName = schedule.Doctor?.Clinic?.Name ?? "Thẩm mỹ viện";
        var clinicAddress = schedule.Doctor?.Clinic?.Address ?? "";
        var scheduleDate = schedule.Date?.ToString("dd/MM/yyyy") ?? "";
        var startTime = schedule.StartTime?.ToString(@"hh\:mm") ?? "";
        var endTime = schedule.EndTime?.ToString(@"hh\:mm") ?? "";

        // Choose the appropriate template based on the reminder type
        if (reminderType == CustomerScheduleReminder.ReminderTypes.MissedAppointment)
            return GetMissedAppointmentTemplate(
                customerName, serviceName, doctorName, clinicName,
                clinicAddress, scheduleDate, startTime, endTime, schedule);

        var reminderTimeText = GetReminderTimeText(reminderType);
        return GetUpcomingAppointmentTemplate(
            customerName, serviceName, doctorName, clinicName,
            clinicAddress, scheduleDate, startTime, endTime,
            reminderTimeText, schedule);
    }

    private static string GetReminderTimeText(string reminderType)
    {
        return reminderType switch
        {
            CustomerScheduleReminder.ReminderTypes.SevenDay => "7 ngày",
            CustomerScheduleReminder.ReminderTypes.ThreeDay => "3 ngày",
            CustomerScheduleReminder.ReminderTypes.OneDay => "1 ngày",
            CustomerScheduleReminder.ReminderTypes.TwoHour => "2 giờ",
            _ => "sắp tới"
        };
    }

    private static MailContent GetUpcomingAppointmentTemplate(
        string customerName, string serviceName, string doctorName,
        string clinicName, string clinicAddress, string scheduleDate,
        string startTime, string endTime, string reminderTimeText,
        CustomerSchedule schedule)
    {
        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Nhắc nhở lịch hẹn</title>
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
            background-color: #f8bbd0;
            color: #880e4f;
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
        .appointment-details {{
            background-color: #fce4ec;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #ec407a;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
            padding-top: 15px;
            border-top: 1px solid #eee;
        }}
        .important-note {{
            background-color: #fff3e0;
            padding: 10px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #ff9800;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Nhắc nhở lịch hẹn</h2>
    </div>
    <div class='content'>
        <p>Kính gửi {customerName},</p>

        <p>Chúng tôi xin gửi lời nhắc nhở về lịch hẹn sắp diễn ra trong <strong>{reminderTimeText}</strong> tới của bạn tại thẩm mỹ viện của chúng tôi.</p>

        <div class='appointment-details'>
            <p><strong>Ngày:</strong> {scheduleDate}</p>
            <p><strong>Thời gian:</strong> {startTime} - {endTime}</p>
            <p><strong>Dịch vụ:</strong> {serviceName}</p>
            <p><strong>Bác sĩ:</strong> {doctorName}</p>
            <p><strong>Địa điểm:</strong> {clinicName}, {clinicAddress}</p>
        </div>

        <div class='important-note'>
            <p><strong>Lưu ý quan trọng:</strong></p>
            <p>Vui lòng đến trước giờ hẹn 15 phút để hoàn tất thủ tục đăng ký.</p>
            <p>Nếu bạn cần thay đổi lịch hẹn, vui lòng liên hệ với chúng tôi trước ít nhất 24 giờ.</p>
        </div>

        <p>Chúng tôi rất mong được phục vụ bạn!</p>

        <p>Trân trọng,</p>
        <p>Đội ngũ {clinicName}</p>
    </div>

    <div class='footer'>
        <p>&copy; {DateTime.Now.Year} {clinicName}. Tất cả các quyền được bảo lưu.</p>
    </div>
</body>
</html>
";

        return new MailContent
        {
            To = schedule.Customer?.Email ?? "",
            Subject = $"Nhắc nhở ({reminderTimeText}): Lịch hẹn tại {clinicName} vào ngày {scheduleDate}",
            Body = body
        };
    }

    private static MailContent GetMissedAppointmentTemplate(
        string customerName, string serviceName, string doctorName,
        string clinicName, string clinicAddress, string scheduleDate,
        string startTime, string endTime, CustomerSchedule schedule)
    {
        var body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Lịch hẹn bị lỡ</title>
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
        .appointment-details {{
            background-color: #fff3e0;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
            border-left: 4px solid #ff9800;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 0.9em;
            color: #777;
            text-align: center;
            padding-top: 15px;
            border-top: 1px solid #eee;
        }}
        .action-button {{
            display: inline-block;
            background-color: #ff9800;
            color: white;
            padding: 10px 20px;
            text-decoration: none;
            border-radius: 5px;
            margin: 15px 0;
            font-weight: bold;
        }}
        .contact-info {{
            background-color: #f5f5f5;
            padding: 15px;
            border-radius: 5px;
            margin: 15px 0;
        }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>Lịch hẹn bị lỡ</h2>
    </div>
    <div class='content'>
        <p>Kính gửi {customerName},</p>
        
        <p>Chúng tôi nhận thấy rằng bạn đã không đến buổi hẹn đã lên lịch tại thẩm mỹ viện của chúng tôi. Chúng tôi hy vọng bạn vẫn khỏe mạnh và mong muốn được phục vụ bạn vào một thời điểm thuận tiện khác.</p>
        
        <div class='appointment-details'>
            <h3>Chi tiết lịch hẹn đã lỡ</h3>
            <p><strong>Ngày:</strong> {scheduleDate}</p>
            <p><strong>Thời gian:</strong> {startTime} - {endTime}</p>
            <p><strong>Dịch vụ:</strong> {serviceName}</p>
            <p><strong>Bác sĩ:</strong> {doctorName}</p>
            <p><strong>Địa điểm:</strong> {clinicName}, {clinicAddress}</p>
        </div>
        
        <p>Chúng tôi hiểu rằng đôi khi có những tình huống không lường trước được có thể xảy ra. Nếu bạn vẫn muốn đặt lại lịch hẹn, vui lòng liên hệ với chúng tôi qua thông tin bên dưới hoặc đăng nhập vào tài khoản của bạn để đặt lịch hẹn mới.</p>
        
        <div class='contact-info'>
            <h3>Thông tin liên hệ</h3>
            <p><strong>Điện thoại:</strong> {schedule.Doctor?.Clinic?.PhoneNumber ?? "Vui lòng liên hệ qua ứng dụng"}</p>
            <p><strong>Email:</strong> {schedule.Doctor?.Clinic?.Email ?? "Vui lòng liên hệ qua ứng dụng"}</p>
        </div>
        
        <p>Chúng tôi rất mong được phục vụ bạn trong thời gian sớm nhất!</p>
        
        <p>Trân trọng,</p>
        <p>Đội ngũ {clinicName}</p>
    </div>
    
    <div class='footer'>
        <p>&copy; {DateTime.Now.Year} {clinicName}. Tất cả các quyền được bảo lưu.</p>
    </div>
</body>
</html>
";

        return new MailContent
        {
            To = schedule.Customer?.Email ?? "",
            Subject = $"Lịch hẹn bị lỡ - {clinicName} - {scheduleDate}",
            Body = body
        };
    }
}