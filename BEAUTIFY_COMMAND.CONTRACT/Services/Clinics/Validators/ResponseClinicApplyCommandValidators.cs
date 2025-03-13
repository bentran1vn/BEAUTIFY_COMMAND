namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics.Validators;
public class ResponseClinicApplyCommandValidators : AbstractValidator<Commands.ResponseClinicApplyCommand>
{
    public ResponseClinicApplyCommandValidators()
    {
        RuleFor(x => x.RequestId).NotNull().NotEmpty();
        RuleFor(x => x.Action).NotNull().GreaterThanOrEqualTo(0).LessThan(3);
    }
}