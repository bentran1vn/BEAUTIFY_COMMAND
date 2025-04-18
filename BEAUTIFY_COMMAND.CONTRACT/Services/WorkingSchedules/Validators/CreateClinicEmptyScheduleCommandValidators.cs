namespace BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules.Validators;
public class CreateClinicEmptyScheduleCommandValidators : AbstractValidator<Commands.CreateClinicEmptyScheduleCommand>
{
    public CreateClinicEmptyScheduleCommandValidators()
    {
        RuleFor(x => x.WorkingDates)
            .NotEmpty()
            .WithMessage("Working dates cannot be empty.");

        RuleForEach(x => x.WorkingDates)
            .ChildRules(workingDate =>
            {
                workingDate.RuleFor(x => x.Date)
                    .NotEmpty()
                    .WithMessage("Date cannot be empty.");

                workingDate.RuleFor(x => x.StartTime)
                    .NotEmpty()
                    .WithMessage("Start time cannot be empty.");

                workingDate.RuleFor(x => x.EndTime)
                    .NotEmpty()
                    .WithMessage("End time cannot be empty.")
                    .GreaterThan(x => x.StartTime)
                    .WithMessage("End time must be greater than start time.");

                workingDate.RuleFor(x => x.Capacity)
                    .GreaterThan(0)
                    .WithMessage("Capacity must be greater than 0.");
            });

        //date must be in the future
        RuleForEach(x => x.WorkingDates)
            .Must(x => x.Date > DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Date must be in the future.");
    }
}