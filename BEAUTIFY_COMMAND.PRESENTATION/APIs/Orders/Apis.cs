using BEAUTIFY_COMMAND.CONTRACT.Services.Orders;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Orders;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "api/v{version:apiVersion}/orders";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Orders")
            .MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost(string.Empty, CreateOrder).RequireAuthorization();
    }

    private static async Task<IResult> CreateOrder(ISender sender,
        [FromForm] Commands.CustomerOrderServiceCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}