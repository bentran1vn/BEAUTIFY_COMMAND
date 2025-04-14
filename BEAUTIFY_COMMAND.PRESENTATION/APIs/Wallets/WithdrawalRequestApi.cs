using BEAUTIFY_COMMAND.CONTRACT.Services.Wallets;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Wallets;
public class WithdrawalRequestApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/wallets/withdrawal-requests";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Wallet Withdrawal Requests")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost(string.Empty, CreateWithdrawalRequest)
            .RequireAuthorization(Constant.Role.CLINIC_STAFF)
            .WithName("Create Withdrawal Request")
            .WithSummary("Create a withdrawal request from a child clinic to its parent clinic");

        /* gr1.MapGet("", GetWithdrawalRequests)
             .RequireAuthorization(RoleNames.ClinicManager)
             .WithName("Get Withdrawal Requests")
             .WithSummary("Get withdrawal requests for a clinic");

         gr1.MapGet("{id:guid}", GetWithdrawalRequestById)
             .RequireAuthorization(RoleNames.ClinicManager)
             .WithName("Get Withdrawal Request By Id")
             .WithSummary("Get a specific withdrawal request by its ID");*/

        gr1.MapPost("{id:guid}/process", ProcessWithdrawalRequest)
            .RequireAuthorization(Constant.Role.CLINIC_ADMIN)
            .WithName("Process Withdrawal Request")
            .WithSummary("Approve or reject a withdrawal request");
    }

    private static async Task<IResult> CreateWithdrawalRequest(
        ISender sender,
        [FromBody] Commands.CreateWithdrawalRequestCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> GetWithdrawalRequests(
        ISender sender,
        [FromQuery] Guid? childClinicId,
        [FromQuery] Guid? parentClinicId,
        [FromQuery] int? status)
    {
        var query = new WithdrawalRequestQueries.GetWithdrawalRequestsQuery(
            childClinicId, parentClinicId, status);

        var result = await sender.Send(query);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> GetWithdrawalRequestById(
        ISender sender,
        Guid id)
    {
        var query = new WithdrawalRequestQueries.GetWithdrawalRequestByIdQuery(id);
        var result = await sender.Send(query);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ProcessWithdrawalRequest(
        ISender sender,
        Guid id,
        [FromBody] ProcessWithdrawalRequestBody body)
    {
        var command = new Commands.ProcessWithdrawalRequestCommand(
            id, body.IsApproved, body.RejectionReason);

        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private class ProcessWithdrawalRequestBody
    {
        public bool IsApproved { get; set; }
        public string? RejectionReason { get; set; }
    }
}