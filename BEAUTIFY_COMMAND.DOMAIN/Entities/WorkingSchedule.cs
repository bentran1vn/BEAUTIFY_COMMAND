using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.WorkingSchedules;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class WorkingSchedule : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid? DoctorClinicId { get; set; }
    public virtual UserClinic? DoctorClinic { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }


    public void WorkingScheduleCreate(Guid DoctorId, Guid ClinicId, string DoctorName,
        List<WorkingSchedule> workingSchedule)
    {
        //map from workingSchedule to WorkingScheduleEntities
        var workingScheduleEntities = workingSchedule.Select(x => new EntityEvent.WorkingScheduleEntity
        {
            Id = x.Id,
            DoctorId = DoctorId,
            ClinicId = ClinicId,
            Date = x.Date,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            IsDeleted = false,
            ModifiedOnUtc = null
        }).ToList();


        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.WorkingScheduleCreated(
            Guid.NewGuid(),
            workingScheduleEntities, DoctorName));
    }

    public void WorkingScheduleDelete(Guid WorkingScheduleId)
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("WorkingScheduleCreate");
        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.WorkingScheduleDeleted(Guid.NewGuid(), WorkingScheduleId));
    }

    public void WorkingScheduleUpdate(List<WorkingSchedule> workingSchedule, string DoctorName)
    {
        //map from workingSchedule to WorkingScheduleEntities
        var workingScheduleEntities = workingSchedule.Select(x => new EntityEvent.WorkingScheduleEntity
        {
            Id = x.Id,
            Date = x.Date,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            IsDeleted = false
        }).ToList();

        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.WorkingScheduleUpdated(
            Guid.NewGuid(),
            workingScheduleEntities, DoctorName));
    }
}