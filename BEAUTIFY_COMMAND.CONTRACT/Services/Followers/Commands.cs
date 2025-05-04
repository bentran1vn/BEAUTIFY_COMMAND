namespace BEAUTIFY_COMMAND.CONTRACT.Services.Followers;

public class Commands
{
    public class FollowBody
    {
        public Guid ClinicId { get; set; }
        public bool IsFollow { get; set; }
    }
    
    public class FollowCommand : FollowBody, ICommand
    {
        public FollowCommand(FollowBody body, Guid userId)
        {
            ClinicId = body.ClinicId;
            IsFollow = body.IsFollow;
            UserId = userId;
        }
        public Guid UserId { get; set; }
    }
}