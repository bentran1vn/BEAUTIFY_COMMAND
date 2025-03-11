using BEAUTIFY_COMMAND.CONTRACT.Services.Survey;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Surveys;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/surveys";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Surveys").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost("", CreateSurvey).DisableAntiforgery();
    }

    private static async Task<IResult> CreateSurvey(ISender sender,
       [FromForm] Commands.CreateSurveyCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}