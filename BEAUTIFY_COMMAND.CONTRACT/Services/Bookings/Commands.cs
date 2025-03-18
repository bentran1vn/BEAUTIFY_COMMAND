namespace BEAUTIFY_COMMAND.CONTRACT.Services.Bookings;
public static class Commands
{
    public record CreateBookingCommand(
        Guid DoctorId,
        TimeSpan StartTime,
        DateOnly BookingDate,
        Guid ClinicId,
        Guid ServiceId,
        List<Guid> ProcedurePriceTypeIds,
        bool IsDefault) : ICommand;
}