using BEAUTIFY_COMMAND.CONTRACT.Services.Payments;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Shared;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Payments;
public class PaymentApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Payments")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost("sepay-payment", TriggerFromHook)
            .WithName("Web Hook for Payments")
            .WithSummary("Web Hook for Payments.");

        gr1.MapPost("subscription", CreateSubscriptionOrder)
            .WithName("Subscription Payments")
            .WithSummary("Subscription Payments.").RequireAuthorization();
        
    }

    private static async Task<IResult> TriggerFromHook(ISender sender,
        [FromBody] Commands.SepayBodyHook command)
    {
        var (type, id) = QrContentParser.TakeOrderIdFromContent(command.content);

        var request = new Commands.TriggerFromHookCommand()
        {
            Id = id,
            TransferAmount = command.transferAmount,
            PaymentDate = command.transactionDate
        };

        if (type.Equals("ORDER"))
        {
            request.Type = 1;
        }

        if (type.Equals("SUB"))
        {
            request.Type = 0;
        }

        var result = await sender.Send(request);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CreateSubscriptionOrder(ISender sender, HttpContext context,
        [FromBody] Commands.SubscriptionOrderBody command)
    {
        var clinicId = context.User.FindFirst(c => c.Type == "ClinicId")?.Value!;

        var result = await sender.Send(
            new Commands.SubscriptionOrderCommand(
                command.SubscriptionId,
                new Guid(clinicId)));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
   
}