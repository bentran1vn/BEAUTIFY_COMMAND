namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Clinics;
public class TestTokenCommandHandler(ICurrentUserService currentUserService)
    : ICommandHandler<CONTRACT.Services.Clinics.Commands.TestTokenCommand>
{
    public async Task<Result> Handle(CONTRACT.Services.Clinics.Commands.TestTokenCommand request,
        CancellationToken cancellationToken)
    {
        return Result.Success(currentUserService.UserId);
    }
}