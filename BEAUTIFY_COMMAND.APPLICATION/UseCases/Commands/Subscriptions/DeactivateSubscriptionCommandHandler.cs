namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Subscriptions;
public class
    DeactivateSubscriptionCommandHandler : ICommandHandler<
    CONTRACT.Services.Subscription.Commands.DeactivateSubscriptionCommand>
{
    private readonly IRepositoryBase<SubscriptionPackage, Guid> _repositoryBase;

    public DeactivateSubscriptionCommandHandler(IRepositoryBase<SubscriptionPackage, Guid> repositoryBase)
    {
        _repositoryBase = repositoryBase;
    }

    public async Task<Result> Handle(CONTRACT.Services.Subscription.Commands.DeactivateSubscriptionCommand request,
        CancellationToken cancellationToken)
    {
        var existingSubscription =
            await _repositoryBase.FindSingleAsync(x => x.Id.Equals(request.Id), cancellationToken);
        if (existingSubscription is null) return Result.Failure(new Error("404", "Subscription not found"));

        if (!existingSubscription.IsActivated)
            return Result.Failure(new Error("400", "Subscription is already de-active"));

        existingSubscription.DeactivateSubscription();
        _repositoryBase.Update(existingSubscription);
        return Result.Success();
    }
}