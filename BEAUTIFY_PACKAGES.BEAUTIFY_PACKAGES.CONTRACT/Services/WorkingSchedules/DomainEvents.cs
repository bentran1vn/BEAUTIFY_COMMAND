using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;

namespace BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.WorkingSchedules;

public static class DomainEvents
{
    public record WorkingScheduleCreated(
        Guid Id,
        List<EntityEvent.WorkingScheduleEntity> WorkingScheduleEntities,
        string DoctorName);

    public record WorkingScheduleDeleted(
        Guid Id,
        Guid WorkingScheduleId);

    public record WorkingScheduleUpdated(
        Guid Id,
        List<EntityEvent.WorkingScheduleEntity> WorkingScheduleEntities,
        string DoctorName);
        
    public record ClinicEmptyScheduleCreated(
        Guid Id,
        List<EntityEvent.WorkingScheduleEntity> WorkingScheduleEntities,
        string ClinicName);
        
    public record ClinicScheduleCapacityChanged(
        Guid Id,
        Guid ShiftGroupId,
        int OldCapacity,
        int NewCapacity,
        List<EntityEvent.WorkingScheduleEntity> WorkingScheduleEntities,
        string ClinicName);
        
    public record DoctorScheduleRegistered(
        Guid Id,
        Guid DoctorId,
        string DoctorName,
        List<EntityEvent.WorkingScheduleEntity> WorkingScheduleEntities);
}
