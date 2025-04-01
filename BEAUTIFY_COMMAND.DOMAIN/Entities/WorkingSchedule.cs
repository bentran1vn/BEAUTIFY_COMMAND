using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.WorkingSchedules;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class WorkingSchedule : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid? DoctorClinicId { get; set; }
    public virtual UserClinic? DoctorClinic { get; set; }

    public Guid? CustomerScheduleId { get; set; }
    public virtual CustomerSchedule? CustomerSchedule { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }


    public void WorkingScheduleCreate(Guid DoctorId, Guid ClinicId, string DoctorName,
        List<WorkingSchedule> workingSchedule, CustomerSchedule? customerSchedule)
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
            ModifiedOnUtc = null,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Note = string.Empty,
            CustomerScheduleId = customerSchedule!.Id,
            CustomerScheduleEntity = new EntityEvent.CustomerScheduleEntity
            {
                Id = customerSchedule.Id,
                CustomerName = customerSchedule.Customer.FirstName + " " + customerSchedule.Customer.LastName,
                StepIndex = customerSchedule.ProcedurePriceType.Procedure.StepIndex.ToString(),
                CustomerId = customerSchedule.CustomerId,
                StartTime = customerSchedule.StartTime.Value,
                EndTime = customerSchedule.EndTime.Value,
                Date = customerSchedule.Date.Value,
                ServiceId = customerSchedule.ServiceId,
                ServiceName = customerSchedule.Service.Name,
                DoctorId = customerSchedule.DoctorId,
                DoctorName = customerSchedule.Doctor.User.FirstName + " " + customerSchedule.Doctor.User.LastName,
                ClinicId = customerSchedule.Doctor.ClinicId,
                ClinicName = customerSchedule.Doctor.Clinic.Name,
                CurrentProcedureName = customerSchedule.ProcedurePriceType.Name,
                Status = customerSchedule.Status,
                CompletedProcedures = [],
                PendingProcedures = []
            }
        }).ToList();


        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.WorkingScheduleCreated(
            Guid.NewGuid(),
            workingScheduleEntities, DoctorName));
    }


    public void WorkingScheduleDelete(Guid WorkingScheduleId)
    {
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
            IsDeleted = false,
            CustomerScheduleId = x.CustomerScheduleId.Value,
        }).ToList();

        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.WorkingScheduleUpdated(
            Guid.NewGuid(),
            workingScheduleEntities, DoctorName));
    }
}