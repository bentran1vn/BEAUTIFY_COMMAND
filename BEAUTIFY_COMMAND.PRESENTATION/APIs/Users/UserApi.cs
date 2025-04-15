using BEAUTIFY_COMMAND.CONTRACT.Services.Users;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

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
            .RequireAuthorization(Constant.Policy.POLICY_DOCTOR_AND_CUSTOMER)
            .WithName("Update User Profile")
            .WithSummary("Update user profile information")
            .WithDescription(
                "Updates the profile information of the currently authenticated user (customer or doctor)");
    }

    private static async Task<IResult> UpdateUserProfile(
        ISender sender,
        [FromForm] Commands.UpdateUserProfileCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}