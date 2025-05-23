﻿using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Clinic;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class UserClinic : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid UserId { get; set; }
    public Guid ClinicId { get; set; }
    public virtual Clinic? Clinic { get; set; }
    public virtual Staff? User { get; set; }

    public virtual ICollection<CustomerSchedule>? CustomerSchedules { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    public void RaiseDoctorFromClinicDeletedEvent(Guid UserClinic)
    {
        RaiseDomainEvent(new DomainEvents.DoctorFromClinicDeleted(Guid.NewGuid(), UserClinic));
    }
}