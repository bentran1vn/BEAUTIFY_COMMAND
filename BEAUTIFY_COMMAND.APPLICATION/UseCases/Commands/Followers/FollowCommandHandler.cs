namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Followers;

public class FollowCommandHandler: ICommandHandler<CONTRACT.Services.Followers.Commands.FollowCommand>
{
    private readonly IRepositoryBase<Follower, Guid> _followerRepository;
    private readonly IRepositoryBase<Clinic, Guid> _clinicRepository;
    private readonly IRepositoryBase<User, Guid> _userRepository;

    public FollowCommandHandler(IRepositoryBase<Follower, Guid> followerRepository, IRepositoryBase<Clinic, Guid> clinicRepository, IRepositoryBase<User, Guid> userRepository)
    {
        _followerRepository = followerRepository;
        _clinicRepository = clinicRepository;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(CONTRACT.Services.Followers.Commands.FollowCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.UserId, cancellationToken);
        
        if (user is null)
            return Result.Failure(new Error("404", "User not found"));
        
        var clinic = await _clinicRepository.FindByIdAsync(request.ClinicId, cancellationToken);
        
        if (clinic is null)
            return Result.Failure(new Error("404", "Clinic not found"));
        
        var follower = await _followerRepository.FindSingleAsync(x => x.UserId.Equals(request.UserId) && x.ClinicId.Equals(request.ClinicId), cancellationToken);

        if (request.IsFollow)
        {
            if (follower == null || follower.IsDeleted)
            {
                var follow = new Follower()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    ClinicId = request.ClinicId
                };
                
                _followerRepository.Add(follow);
            }
            else
            {
                follower.IsDeleted = false;
            }
        }
        else
        {
            if(follower != null && !follower.IsDeleted)
            {
                _followerRepository.Remove(follower);
            }
        }

        return Result.Success("Followed action successfully");
    }
}