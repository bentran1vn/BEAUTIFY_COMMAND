using System.ComponentModel.DataAnnotations;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;

public class CustomerScheduleReminder : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid CustomerScheduleId { get; set; }
    public virtual CustomerSchedule? CustomerSchedule { get; set; }

    [MaxLength(20)]
    public string ReminderType { get; set; } = string.Empty;

    public DateTimeOffset SentOnUtc { get; set; }

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    // Reminder types as constants
    public static class ReminderTypes
    {
        // Upcoming appointment reminders
        public const string SevenDay = "7_DAY";
        public const string ThreeDay = "3_DAY";
        public const string OneDay = "1_DAY";
        public const string TwoHour = "2_HOUR";

        // Missed appointment notification
        public const string MissedAppointment = "MISSED";
    }
}
