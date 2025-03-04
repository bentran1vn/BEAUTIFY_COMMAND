using BEAUTIFY_COMMAND.CONTRACT.Services.Subscription;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Subscriptions;
public class SubscriptionApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/subscriptions";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Subscriptions")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost(string.Empty, CreateSubscription).RequireAuthorization();
        gr1.MapPut("{id:guid}", UpdateSubscription).RequireAuthorization();
        gr1.MapDelete("{id:guid}", DeleteSubscription).RequireAuthorization();
        gr1.MapPut("change-status/{id:guid}", ChangeStatusSubscription);
    }

    private static async Task<IResult> CreateSubscription(ISender sender,
        Commands.CreateSubscriptionCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateSubscription(ISender sender,
        Guid id,
        [FromBody] Commands.UpdateSubscriptionCommand command)
    {
        var result = await sender.Send(command with { Id = id });
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeleteSubscription(ISender sender,
        Guid id)
    {
        var result = await sender.Send(new Commands.DeleteSubscriptionCommand(id));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ChangeStatusSubscription(ISender sender,
        Guid id)
    {
        var result = await sender.Send(new Commands.ChangeStatusSubscriptionCommand(id));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}