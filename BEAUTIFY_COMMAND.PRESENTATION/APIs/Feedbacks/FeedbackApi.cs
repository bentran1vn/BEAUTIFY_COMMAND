using BEAUTIFY_COMMAND.CONTRACT.Services.Feedbacks;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Feedbacks;

public class FeedbackApi: ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/feedbacks";
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Feedbacks")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost("", CreateFeedback);
    }
    
    private static async Task<IResult> CreateFeedback(ISender sender,
        [FromBody] Commands.CreateFeedbackCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}