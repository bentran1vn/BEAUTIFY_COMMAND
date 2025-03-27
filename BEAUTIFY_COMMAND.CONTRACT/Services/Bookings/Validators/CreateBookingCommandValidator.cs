namespace BEAUTIFY_COMMAND.CONTRACT.Services.Bookings.Validators;
public class CreateBookingCommandValidator : AbstractValidator<Commands.CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.BookingDate)
            .NotEmpty()
            .WithMessage("Booking date is required")
            .Must(date => date >= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Booking date cannot be in the past");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Start time is required")
            .Must((command, startTime) =>
            {
                var now = DateTime.Now.TimeOfDay;
                if (command.BookingDate == DateOnly.FromDateTime(DateTime.Today))
                {
                    return startTime > now.Add(TimeSpan.FromMinutes(120));
                }

                return true;
            })
            .WithMessage("Start time must be at least 2 hours from now for today's bookings");

        RuleFor(x => x)
            .Must(command =>
            {
                var bookingDateTime = command.BookingDate.ToDateTime(TimeOnly.FromTimeSpan(command.StartTime));
                return bookingDateTime.DayOfWeek != DayOfWeek.Sunday;
            })
            .WithMessage("Bookings are not allowed on Sundays")
            .OverridePropertyName("BookingDate");
    }
}