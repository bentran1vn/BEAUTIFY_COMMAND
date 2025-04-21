namespace BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules.Validators;
public class CreateClinicEmptyScheduleCommandValidators : AbstractValidator<Commands.CreateClinicEmptyScheduleCommand>
{
    public CreateClinicEmptyScheduleCommandValidators()
    {
        RuleForEach(x => x.WorkingDates)
            .ChildRules(workingDate =>
            {
                workingDate.RuleFor(x => x.Date)
                    .NotEmpty()
                    .WithMessage("Date cannot be empty.");

                /* workingDate.RuleFor(x => x.StartTime)
                     .NotEmpty()
                     .WithMessage("Start time cannot be empty.");

                 workingDate.RuleFor(x => x.EndTime)
                     .NotEmpty()
                     .WithMessage("End time cannot be empty.")
                     .Must((model, endTime) =>
                     {
                         // Parse strings to TimeOnly objects before comparison
                         if (TimeOnly.TryParse(model.StartTime, out var startTimeObj) &&
                             TimeOnly.TryParse(endTime, out var endTimeObj))
                         {
                             // Compare TimeOnly objects properly
                             return endTimeObj > startTimeObj || endTimeObj == new TimeOnly(0, 0);
                         }

                         return false; // If parsing fails, validation fails
                     })
                     .WithMessage("End time must be greater than start time or set to midnight (00:00).");
 */
                workingDate.RuleFor(x => x.Capacity)
                    .GreaterThan(0)
                    .WithMessage("Capacity must be greater than 0.");
            });

        // Date must be in the future
        RuleForEach(x => x.WorkingDates)
            .Must(x =>
            {
                // No parsing needed since x.Date is already DateOnly
                var dateObj = x.Date;
                if (true)
                {
                    return dateObj > DateOnly.FromDateTime(DateTime.Now);
                }

                return false; // If parsing fails, validation fails
            })
            .WithMessage("Date must be in the future.");
    }
}