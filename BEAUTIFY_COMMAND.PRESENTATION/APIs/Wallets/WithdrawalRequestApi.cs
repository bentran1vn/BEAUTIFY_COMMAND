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
            .RequireAuthorization(Constant.Policy.POLICY_CLINIC_ADMIN_AND_CLINIC_STAFF)
            .WithName("Create Withdrawal Request")
            .WithSummary("Create a withdrawal request from a child clinic to its parent clinic");


        gr1.MapPatch("{id:guid}", ProcessWithdrawalRequest)
            .RequireAuthorization(Constant.Role.CLINIC_ADMIN)
            .WithName("Update Withdrawal Request")
            .WithSummary("Approve or reject a withdrawal request");
        gr1.MapPatch("{id:guid}/system", SystemAdminProcessWithdrawalRequest)
            .RequireAuthorization(Constant.Role.SYSTEM_ADMIN)
            .WithName("System Admin Update Withdrawal Request")
            .WithSummary("Approve or reject a withdrawal request by system admin");
        gr1.MapPatch("{id:guid}/status", SystemAdminAfterTransferWallet)
            .RequireAuthorization(Constant.Role.SYSTEM_ADMIN)
            .WithName("Update Withdrawal Request Status")
            .WithSummary("Update the status of a withdrawal request by system admin after transfer");
        gr1.MapPost("/customer-withdrawals", CustomerWithdrawFromWallet)
            .RequireAuthorization(Constant.Role.CUSTOMER)
            .WithName("Customer Withdraw From Wallet")
            .WithSummary("Withdraw money from wallet to bank account.");
    }

    private static async Task<IResult> SystemAdminAfterTransferWallet(
        ISender sender,
        Guid id,
        [FromBody] IFormFile? image)
    {
        var command = new Commands.SystemAdminAfterTransferWalletCommand(id, image);
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CreateWithdrawalRequest(
        ISender sender,
        [FromBody] Commands.CreateWithdrawalRequestCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CustomerWithdrawFromWallet(
        ISender sender,
        [FromBody] Commands.CustomerWithdrawFromWalletCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> SystemAdminProcessWithdrawalRequest(
        ISender sender,
        Guid id,
        [FromBody] ProcessWithdrawalRequestBody body)
    {
        var command = new Commands.SystemAdminProcessWithdrawalRequestCommand(
            id, body.IsApproved, body.RejectionReason);

        var result = await sender.Send(command);
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