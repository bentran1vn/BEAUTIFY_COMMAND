using BEAUTIFY_COMMAND.CONTRACT.Services.Payments;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.Extensions.Logging;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Payments;
public class PaymentApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/payments";
    private readonly ILogger<PaymentApi> _logger ;

    public PaymentApi(ILogger<PaymentApi> logger)
    {
        _logger = logger;
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Payments")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapGet("payOs/success", HandlePaySuccess);

        gr1.MapPost("sepay-payment", TriggerFromHook)
            .WithName("Web Hook for Payments")
            .WithSummary("Web Hook for Payments.");

        gr1.MapPost("subscription", CreateSubscriptionOrder)
            .WithName("Subscription Payments")
            .WithSummary("Subscription Payments.")
            .RequireAuthorization();

        gr1.MapPost("subscription/over", CreateSubscriptionOverOrder)
            .WithName("Subscription Over Payments")
            .WithSummary("Subscription Over Payments.")
            .RequireAuthorization();

        gr1.MapPost("order/", CustomerOrderPayment)
            .WithName("Customer Order Payments")
            .WithSummary("Customer Order Payments.")
            .RequireAuthorization();
        ;

        gr1.MapPost("wallets/top-ups", CustomerTopUpWallet)
            .RequireAuthorization(Constant.Role.CUSTOMER)
            .WithName("Top Up Wallet")
            .WithSummary("Top Up Wallet.");
    }

    private static async Task<IResult> HandlePaySuccess(ISender sender, [FromQuery]Guid transactionId)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine(transactionId);
        return Results.Empty;
    }

    private static async Task<IResult> TriggerFromHook(ISender sender,
        [FromBody] Commands.SepayBodyHook command)
    {
        var (type, id) = QrContentParser.TakeOrderIdFromContent(command.content);

        var request = new Commands.TriggerFromHookCommand
        {
            Id = id,
            TransferAmount = command.transferAmount,
            PaymentDate = command.transactionDate
        };

        request.Type = type switch
        {
            "OVER" => 4,
            "WITHDRAWAL" => 3,
            "WALLET" => 2,
            "ORDER" => 1,
            "SUB" => 0,
            _ => request.Type
        };

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
                new Guid(clinicId),
                command.CurrentAmount));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CreateSubscriptionOverOrder(
        ISender sender, HttpContext context,
        [FromBody] Commands.SubscriptionOverOrderBody command)
    {
        var clinicId = context.User.FindFirst(c => c.Type == "ClinicId")?.Value!;

        var result = await sender.Send(new Commands.SubscriptionOverOrderCommand(
            command.SubscriptionId,
            new Guid(clinicId),
            command.CurrentAmount,
            command.AdditionBranch,
            command.AdditionLiveStream));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CustomerOrderPayment(ISender sender,
        Commands.CustomerOrderPaymentCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CustomerTopUpWallet(
        ISender sender,
        [FromBody] CONTRACT.Services.Wallets.Commands.CustomerTopUpWalletCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}