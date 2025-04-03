namespace BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;
public static class Command
{
    public record CustomerScheduleCreateCommand(
        string CustomerId,
        string ServiceId,
        string DoctorId,
        string Date,
        string Time,
        string Note
    ) : ICommand;


    public record UpdateCustomerScheduleAfterPaymentCompletedCommand(Guid CustomerScheduleId, string Status) : ICommand;


    public record StaffUpdateCustomerScheduleStatusAfterCheckInCommand(
        Guid CustomerScheduleId,
        string Status
    ) : ICommand;

    public record GenerateCustomerScheduleAfterPaymentCompletedCommand(
        Guid CustomerScheduleId
    ) : ICommand;

    public record DoctorUpdateCustomerScheduleNoteCommand(
        Guid CustomerScheduleId,
        string DoctorNote
    ) : ICommand;
}