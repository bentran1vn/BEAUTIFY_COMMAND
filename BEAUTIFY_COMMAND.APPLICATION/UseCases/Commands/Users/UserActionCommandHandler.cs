namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Users;

public class UserActionCommandHandler : ICommandHandler<CONTRACT.Services.Users.Commands.UserActionCommand>
{
    private readonly IRepositoryBase<User, Guid> _userRepositoryBase;

    public UserActionCommandHandler(IRepositoryBase<User, Guid> userRepositoryBase)
    {
        _userRepositoryBase = userRepositoryBase;
    }

    public async Task<Result> Handle(CONTRACT.Services.Users.Commands.UserActionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepositoryBase.FindByIdAsync(request.UserId, cancellationToken);

        if (user == null || user.IsDeleted)
        {
            return Result.Failure(new Error("404", "User not found"));
        }

        if (request.IsActive)
        {
            user.Status = 1;
        }
        else
        {
            user.Status = 3;
        }
        
        return Result.Success("User Action Successfully !");
    }
}