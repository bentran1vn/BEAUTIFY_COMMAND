namespace BEAUTIFY_COMMAND.APPLICATION.UseCases.Commands.Subscriptions;
public class
    ActivateSubscriptionCommandHandler(IRepositoryBase<SubscriptionPackage, Guid> repositoryBase) : ICommandHandler<
    CONTRACT.Services.Subscription.Commands.ChangeSubscriptionActivationCommand>
{
    public async Task<Result> Handle(
        CONTRACT.Services.Subscription.Commands.ChangeSubscriptionActivationCommand request,
        CancellationToken cancellationToken)
    {
        var existingSubscription =
            await repositoryBase.FindSingleAsync(x => x.Id.Equals(request.Id), cancellationToken);
        if (existingSubscription is null) return Result.Failure(new Error("404", "Subscription not found"));

        existingSubscription.ChangeSubscriptionActivation();
        repositoryBase.Update(existingSubscription);
        return Result.Success();
    }
}