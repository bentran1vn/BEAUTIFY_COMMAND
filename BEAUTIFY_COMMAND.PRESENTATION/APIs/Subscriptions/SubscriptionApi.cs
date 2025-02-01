using BEAUTIFY_COMMAND.CONTRACT.Services.Subscription;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Subscriptions;
public class SubscriptionApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/subscriptions";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Subscriptions")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost("", CreateSubscription);
        gr1.MapPut("", UpdateSubscription);
        gr1.MapDelete("", DeleteSubscription);
        gr1.MapPut("activate", ActivateSubscription);
        gr1.MapPut("deactivate", DeactivateSubscription);
    }

    private static async Task<IResult> CreateSubscription(ISender sender,
        [FromBody] Commands.CreateSubscriptionCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateSubscription(ISender sender,
        [FromBody] Commands.UpdateSubscriptionCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeleteSubscription(ISender sender,
        [FromBody] Commands.DeleteSubscriptionCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ActivateSubscription(ISender sender,
        [FromBody] Commands.ActivateSubscriptionCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeactivateSubscription(ISender sender,
        [FromBody] Commands.DeactivateSubscriptionCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}