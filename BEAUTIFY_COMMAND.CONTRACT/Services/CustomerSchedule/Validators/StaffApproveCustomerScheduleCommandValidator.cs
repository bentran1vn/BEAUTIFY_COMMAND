namespace BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule.Validators;
internal sealed class
    StaffApproveCustomerScheduleCommandValidator : AbstractValidator<Command.StaffApproveCustomerScheduleCommand>
{
    public StaffApproveCustomerScheduleCommandValidator()
    {
        RuleFor(x => x.CustomerScheduleId)
            .NotEmpty()
            .WithMessage("Customer schedule id is required");
        RuleFor(x => x.Status)
            .NotEmpty()
            .Must(x => x is "Approved" or "Rejected")
            .WithMessage("Status must be either Approved or Rejected");
    }
}