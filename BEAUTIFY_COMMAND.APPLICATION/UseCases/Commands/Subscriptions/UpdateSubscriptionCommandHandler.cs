namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Subscriptions;
public class UpdateSubscriptionCommandHandler(IRepositoryBase<SubscriptionPackage, Guid> repositoryBase)
    : ICommandHandler<CONTRACT.Services.Subscription.Commands.UpdateSubscriptionCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.Subscription.Commands.UpdateSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Fetch existing subscription
        var existingSubscription =
            await repositoryBase.FindSingleAsync(
                x => x.Id.Equals(request.Id),
                cancellationToken);

        if (existingSubscription is null) return Result.Failure(new Error("404", "Subscription not found"));

        // 2. Check if the new name already exists on a different subscription
        var duplicateNameSubscription = await repositoryBase.FindSingleAsync(
            x => x.Name.Equals(request.Name) && x.Id != request.Id,
            cancellationToken);

        if (duplicateNameSubscription is not null)
            return Result.Failure(new Error("422", "Subscription name already exists"));

        existingSubscription.Name = request.Name;
        existingSubscription.Description = request.Description;
        existingSubscription.Price = request.Price;
        existingSubscription.Duration = request.Duration;
        existingSubscription.LimitBranch = request.LimitBranch;
        existingSubscription.LimitLiveStream = request.LimitLiveStream;
        existingSubscription.PriceMoreBranch = request.PriceBranchAddition;
        existingSubscription.PriceMoreLivestream = request.PriceLiveStreamAddition;
        existingSubscription.EnhancedViewer = request.EnhancedViewer;
        // 4. Save updates via repository
        repositoryBase.Update(existingSubscription);

        return Result.Success();
    }
}