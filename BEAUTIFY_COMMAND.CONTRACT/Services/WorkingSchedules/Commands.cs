using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules;
public static class Commands
{
    public record CreateWorkingScheduleCommand(Guid DoctorId, List<WorkingDate> WorkingDates) : ICommand;

    public record DeleteWorkingScheduleCommand(Guid WorkingScheduleId) : ICommand;

    public record UpdateWorkingScheduleCommand(List<UpdateWorkingDate> WorkingDates) : ICommand;

    public class WorkingDate
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class UpdateWorkingDate : WorkingDate
    {
        public Guid WorkingScheduleId { get; set; }
    }
}