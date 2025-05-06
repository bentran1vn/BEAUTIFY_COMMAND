namespace BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;
public static class Command
{
    public record StaffCancelCustomerScheduleAfterFirstStepCommand(
        Guid CustomerScheduleId,
        Guid OrderId
    ) : ICommand;

    public record StaffRefundCustomerScheduleCommand(
        Guid CustomerScheduleId
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

    public record CustomerRequestScheduleCommand(Guid CustomerScheduleId, DateOnly Date, TimeSpan StartTime) : ICommand;


    public record StaffUpdateCustomerScheduleTimeCommand(
        Guid CustomerScheduleId,
        DateOnly Date,
        bool IsNext,
        TimeSpan StartTime
    ) : ICommand;

    public record StaffApproveCustomerScheduleCommand(
        Guid CustomerScheduleId,
        string Status
    ) : ICommand;
}