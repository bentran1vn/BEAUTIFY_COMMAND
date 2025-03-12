using BEAUTIFY_COMMAND.CONTRACT.Services.Survey;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Surveys;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/surveys";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Surveys").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost("", CreateSurvey) .WithSummary("1 - Multiple Choice == 2 - Single Choice == 3 - Text\n ").DisableAntiforgery();
    }

    private static async Task<IResult> CreateSurvey(ISender sender,
       [FromBody] Commands.CreateSurveyCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}