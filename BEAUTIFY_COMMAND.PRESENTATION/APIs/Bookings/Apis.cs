using BEAUTIFY_COMMAND.CONTRACT.Services.Bookings;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Bookings;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "api/v{version:apiVersion}/bookings";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Bookings")
            .MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost("", CreateBooking).RequireAuthorization()
            .WithDescription("If is default is true then dont need to pass procedure price types");
    }

    private static async Task<IResult> CreateBooking(ISender sender, Commands.CreateBookingCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}