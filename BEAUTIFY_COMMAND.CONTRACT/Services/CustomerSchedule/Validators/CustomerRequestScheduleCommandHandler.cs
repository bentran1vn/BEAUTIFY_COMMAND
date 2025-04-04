namespace BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule.Validators;
public class CustomerRequestScheduleCommandHandler : AbstractValidator<Command.CustomerRequestScheduleCommand>
{
    public CustomerRequestScheduleCommandHandler()
    {
        RuleFor(x => x.CustomerScheduleId)
            .NotEmpty()
            .WithMessage("Customer schedule ID is required.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required.")
            .Must(date => date >= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date must be today or in the future.")
            .Must(date => date.DayOfWeek != DayOfWeek.Sunday)
            .WithMessage("Date cannot be on Sunday.");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Start time is required.")
            .Must((command, time) =>
            {
                var now = DateTime.UtcNow;
                var scheduledDateTime = new DateTime(command.Date.Year, command.Date.Month, command.Date.Day,
                    time.Hours, time.Minutes, 0);
                return scheduledDateTime > now.AddHours(2);
            })
            .WithMessage("Start time must be at least 2 hours from now.")
            .Must(time => time.Hours is >= 8 and <= 20)
            .WithMessage("Start time must be between 08:00 and 20:00.");
    }
}