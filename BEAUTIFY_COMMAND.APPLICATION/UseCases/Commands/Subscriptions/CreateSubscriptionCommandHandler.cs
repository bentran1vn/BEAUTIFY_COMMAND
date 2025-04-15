namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Subscriptions;
internal sealed class CreateSubscriptionCommandHandler(IRepositoryBase<SubscriptionPackage, Guid> _repositoryBase)
    : ICommandHandler<CONTRACT.Services.Subscription.Commands.CreateSubscriptionCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.Subscription.Commands.CreateSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        // Trim the name once and reuse it
        var trimmedName = request.Name.Trim();

        // Check for existing subscription
        var existingSubscription = await _repositoryBase.FindSingleAsync(
            x => x.Name.Equals(trimmedName), // Case-insensitive comparison
            cancellationToken);

        if (existingSubscription != null) return Result.Failure(new Error("400", "Subscription already exists"));

        var subscription = new SubscriptionPackage
        {
            Name = trimmedName,
            Description = request.Description,
            Price = request.Price,
            Duration = request.Duration,
            LimitBranch = request.LimitBranches,
            LimitLiveStream = request.LimitLiveStream,
            EnhancedViewer = request.EnhancedView,
            PriceMoreBranch = request.PriceBranchAddition,
            PriceMoreLivestream = request.PriceLiveStreamAddition,
            IsActivated = false
        };

        _repositoryBase.Add(subscription);


        return Result.Success();
    }
}