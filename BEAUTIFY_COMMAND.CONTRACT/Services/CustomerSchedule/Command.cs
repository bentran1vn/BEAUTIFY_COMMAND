using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;

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
}