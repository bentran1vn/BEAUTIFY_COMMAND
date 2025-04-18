namespace BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules;
public static class Commands
{
    public record CreateWorkingScheduleCommand(Guid DoctorId, List<WorkingDate> WorkingDates) : ICommand;

    public record DeleteWorkingScheduleCommand(Guid WorkingScheduleId) : ICommand;

    public record UpdateWorkingScheduleCommand(List<UpdateWorkingDate> WorkingDates) : ICommand;
    
    public record CreateClinicEmptyScheduleCommand(Guid ClinicId, List<WorkingDateWithCapacity> WorkingDates) : ICommand;
    
    public record DoctorRegisterScheduleCommand(Guid DoctorId, List<Guid> WorkingScheduleIds) : ICommand;

    public class WorkingDate
    {
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    
    public class WorkingDateWithCapacity : WorkingDate
    {
        public int Capacity { get; set; } = 1; // Default to 1 doctor per slot
    }

    public class UpdateWorkingDate : WorkingDate
    {
        public Guid WorkingScheduleId { get; set; }
    }
}
