using System.ComponentModel.DataAnnotations;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.CustomerSchedules;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class CustomerSchedule : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid CustomerId { get; set; }
    public virtual User? Customer { get; set; }
    public Guid ServiceId { get; set; }
    public virtual Service? Service { get; set; }
    public Guid DoctorId { get; set; }
    public virtual UserClinic? Doctor { get; set; }

    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
    public DateOnly? Date { get; set; }

    [MaxLength(50)] public string? Status { get; set; }
    public Guid? ProcedurePriceTypeId { get; set; }
    public virtual ProcedurePriceType? ProcedurePriceType { get; set; }
    public Guid? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    [MaxLength(2000)] public string? DoctorNote { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public void Create(CustomerSchedule? customerSchedule)
    {
        //check endtime and start time and date null otherwise send that data

        var entity = new EntityEvent.CustomerScheduleEntity
        {
            Id = customerSchedule.Id,
            CustomerName = customerSchedule.Customer.FirstName + " " + customerSchedule.Customer.LastName,
            OrderId = customerSchedule.OrderId.Value,
            CustomerId = customerSchedule.CustomerId,
            StartTime = customerSchedule.StartTime,
            EndTime = customerSchedule.EndTime,
            Date = customerSchedule.Date,
            ServiceId = customerSchedule.ServiceId,
            ServiceName = customerSchedule.Service.Name,
            DoctorId = customerSchedule.DoctorId,
            DoctorName = customerSchedule.Doctor.User.FirstName + " " + customerSchedule.Doctor.User.LastName,
            ClinicId = customerSchedule.Doctor.ClinicId,
            ClinicName = customerSchedule.Doctor.Clinic.Name,
            DoctorNote = customerSchedule.DoctorNote,
            CurrentProcedure = new EntityEvent.ProcedurePriceTypeEntity
            {
                Name = customerSchedule.ProcedurePriceType.Name,
                Id = customerSchedule.ProcedurePriceTypeId.Value,
                StepIndex = customerSchedule.ProcedurePriceType.Procedure.StepIndex.ToString(),
                DateCompleted = customerSchedule.Date,
                Duration = 0,
            },
            Status = customerSchedule.Status,
            CompletedProcedures = [],
            PendingProcedures = []
        };

        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.CustomerScheduleCreated(Guid.NewGuid(), entity));
    }

    public void UpdateCustomerScheduleStatus(Guid customerScheduleId, string status)
    {
        // Raise the domain event
        RaiseDomainEvent(
            new DomainEvents.CustomerScheduleUpdateAfterPaymentCompleted(Guid.NewGuid(), customerScheduleId, status));
    }

    public void UpdateCustomerScheduleNote(Guid customerScheduleId, string note)
    {
        // Raise the domain event
        RaiseDomainEvent(
            new DomainEvents.CustomerScheduleUpdatedDoctorNote(Guid.NewGuid(), customerScheduleId, note));
    }

    public void CustomerScheduleUpdateDateAndTime(CustomerSchedule customerSchedule)
    {
        RaiseDomainEvent(new DomainEvents.CustomerScheduleUpdateDateAndTimeAndStatus(Guid.NewGuid(),
            customerSchedule.Id, customerSchedule.StartTime.Value, customerSchedule.EndTime.Value,
            customerSchedule.Date.Value, customerSchedule.Status));
    }
}