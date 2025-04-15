using BEAUTIFY_COMMAND.CONTRACT.Services.Customers;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.PRESENTATION;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Users;
public class UserApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/users";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Users")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPut("profile", UpdateUserProfile)
            .DisableAntiforgery()
            .RequireAuthorization(Constant.Role.CUSTOMER)
            .WithName("Update User Profile")
            .WithSummary("Update user profile information")
            .WithDescription("Updates the profile information of the currently authenticated user");
    }

    private static async Task<IResult> UpdateUserProfile(
        ISender sender,
        [FromForm] Commands.UpdateCustomerCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}
