namespace BEAUTIFY_COMMAND.CONTRACT.Services.Bookings;
public static class Commands
{
    public record CreateBookingCommand(
        Guid DoctorId,
        TimeSpan StartTime,
        DateTimeOffset BookingDate,
        Guid ClinicId,
        Guid ServiceId,
        List<Guid> ProcedurePriceTypeIds) : ICommand;
}