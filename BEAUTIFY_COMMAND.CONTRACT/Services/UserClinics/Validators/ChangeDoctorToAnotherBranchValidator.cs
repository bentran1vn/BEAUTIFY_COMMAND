namespace BEAUTIFY_COMMAND.CONTRACT.Services.UserClinics.Validators;
public class ChangeDoctorToAnotherBranchValidator : AbstractValidator<Commands.ChangeDoctorToAnotherBranchCommand>
{
    public ChangeDoctorToAnotherBranchValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage("DoctorId is required");

        RuleFor(x => x.OldBranchId)
            .NotEqual(x => x.NewBranchId)
            .WithMessage("Two branches cannot be the same");
    }
}