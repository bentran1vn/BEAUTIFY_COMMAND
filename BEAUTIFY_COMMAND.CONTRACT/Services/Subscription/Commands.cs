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
        decimal PriceBranchAddition,
        decimal PriceLiveStreamAddition,
        int EnhancedView) : ICommand;

    public record UpdateSubscriptionCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        int Duration,
        bool IsActivated,
        int LimitBranch,
        int LimitLiveStream,
        decimal PriceBranchAddition,
        decimal PriceLiveStreamAddition,
        int EnhancedViewer)
        : ICommand;

    public record DeleteSubscriptionCommand(Guid Id) : ICommand;

    public record ChangeStatusSubscriptionCommand(Guid Id) : ICommand;
}