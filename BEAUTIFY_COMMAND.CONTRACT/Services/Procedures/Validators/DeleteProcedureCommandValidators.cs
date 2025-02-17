using FluentValidation;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Procedures.Validators;

public class DeleteProcedureCommandValidators: AbstractValidator<Commands.DeleteProcedureCommand>
{
    public DeleteProcedureCommandValidators()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}