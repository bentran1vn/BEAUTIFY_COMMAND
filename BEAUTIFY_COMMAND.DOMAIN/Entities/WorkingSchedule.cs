using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.WorkingSchedules;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class WorkingSchedule : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid? ClinicId { get; set; }
    public virtual Clinic? Clinic { get; set; }
    public Guid? DoctorId { get; set; }
    public virtual Staff? Doctor { get; set; }

    public Guid? CustomerScheduleId { get; set; }
    public virtual CustomerSchedule? CustomerSchedule { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Guid? ShiftGroupId { get; set; } // Added to group schedules in the same shift
    public int? ShiftCapacity { get; set; } // Added to track how many doctors can work in this shift
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }


    public void CreateEmptyClinicSchedule(Guid ClinicId, string ClinicName, List<WorkingSchedule> workingSchedules)
    {
        // Map from workingSchedule to WorkingScheduleEntities for empty schedules
        var workingScheduleEntities = workingSchedules.Select(x => new EntityEvent.WorkingScheduleEntity
        {
            Id = x.Id,
            ClinicId = ClinicId,
            Date = x.Date,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            IsDeleted = false,
            ModifiedOnUtc = null,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Note = string.Empty,
            ShiftGroupId = x.ShiftGroupId,
            ShiftCapacity = x.ShiftCapacity
        }).ToList();

        // Raise the domain event for empty schedules
        RaiseDomainEvent(new DomainEvents.ClinicEmptyScheduleCreated(
            Guid.NewGuid(),
            Guid.NewGuid(),
            workingScheduleEntities,
            ClinicName));
    }

    public void ChangeShiftCapacity(Guid ClinicId, string ClinicName, Guid ShiftGroupId,
        int OldCapacity, int NewCapacity, List<WorkingSchedule> affectedSchedules)
    {
        // Map from workingSchedule to WorkingScheduleEntities for affected schedules
        var workingScheduleEntities = affectedSchedules.Select(x => new EntityEvent.WorkingScheduleEntity
        {
            Id = x.Id,
            ClinicId = ClinicId,
            Date = x.Date,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            IsDeleted = false,
            ModifiedOnUtc = null,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Note = string.Empty,
            ShiftGroupId = x.ShiftGroupId,
            ShiftCapacity = x.ShiftCapacity,
            DoctorId = x.DoctorId,
        }).ToList();

        // Raise the domain event for capacity change
        RaiseDomainEvent(new DomainEvents.ClinicScheduleCapacityChanged(
            Guid.NewGuid(),
            ShiftGroupId,
            OldCapacity,
            NewCapacity,
            workingScheduleEntities));
    }

    public void RegisterDoctorSchedule(Guid DoctorId, string DoctorName, List<WorkingSchedule> registeredSchedules)
    {
        // Map from workingSchedule to WorkingScheduleEntities for registered schedules
        var workingScheduleEntities = registeredSchedules.Select(x => new EntityEvent.WorkingScheduleEntity
        {
            Id = x.Id,
            DoctorId = DoctorId,
            ClinicId = x.ClinicId ?? Guid.Empty,
            Date = x.Date,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            IsDeleted = false,
            ModifiedOnUtc = null,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Note = string.Empty,
            ShiftGroupId = x.ShiftGroupId,
            ShiftCapacity = x.ShiftCapacity
        }).ToList();

        // Raise the domain event for doctor schedule registration
        RaiseDomainEvent(new DomainEvents.DoctorScheduleRegistered(
            Guid.NewGuid(),
            DoctorId,
            DoctorName,
            workingScheduleEntities));
    }

    #region NoUse

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
            CustomerScheduleId = x.CustomerScheduleId.Value
        }).ToList();

        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.WorkingScheduleUpdated(
            Guid.NewGuid(),
            workingScheduleEntities, DoctorName));
    }

    public void WorkingScheduleCreate(Guid DoctorId, Guid ClinicId, string DoctorName,
        List<WorkingSchedule> workingSchedule, CustomerSchedule? customerSchedule)
    {
        //map from workingSchedule to WorkingScheduleEntities
        var workingScheduleEntities = workingSchedule.Select(x => new EntityEvent.WorkingScheduleEntity
        {
            Id = x.Id,
            ClinicId = ClinicId,
            DoctorId = DoctorId,
            Date = x.Date,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            IsDeleted = false,
            ModifiedOnUtc = null,
            Status = Constant.OrderStatus.ORDER_PENDING,
            Note = string.Empty,
            ShiftGroupId = x.ShiftGroupId,
            ShiftCapacity = x.ShiftCapacity,
            CustomerScheduleId = customerSchedule!.Id,
            CustomerScheduleEntity = new EntityEvent.CustomerScheduleEntity
            {
                Id = customerSchedule.Id,
                CustomerName = customerSchedule.Customer.FirstName + " " + customerSchedule.Customer.LastName,
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
                CurrentProcedure = new EntityEvent.ProcedurePriceTypeEntity
                {
                    Name = customerSchedule.ProcedurePriceType.Name,
                    Id = customerSchedule.ProcedurePriceTypeId.Value,
                    StepIndex = customerSchedule.ProcedurePriceType.Procedure.StepIndex.ToString(),
                    DateCompleted = (DateOnly)customerSchedule.Date,
                    Duration = 0
                },
                Status = customerSchedule.Status,
            }
        }).ToList();


        // Raise the domain event
        RaiseDomainEvent(new DomainEvents.WorkingScheduleCreated(
            Guid.NewGuid(),
            workingScheduleEntities, DoctorName));
    }

    #endregion
}