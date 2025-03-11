namespace BEAUTIFY_COMMAND.CONTRACT.Services.Subscription;
public static class Commands
{
    public record CreateSubscriptionCommand(
        string Name,
        string Description,
        decimal Price,
        int Duration,
        int LimitBranches,
        int LimitLiveStream,
        int EnhancedView) : ICommand;

    public record UpdateSubscriptionCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        int Duration,
        bool IsActivated,
        int LimitBranches,
        int LimitLiveStream,
        int EnhancedView)
        : ICommand;

    public record DeleteSubscriptionCommand(Guid Id) : ICommand;

    public record ChangeStatusSubscriptionCommand(Guid Id) : ICommand;
}