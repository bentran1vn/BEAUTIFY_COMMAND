using BEAUTIFY_COMMAND.CONTRACT.Services.SurveyQuestions;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.SurveyQuestions;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/survey-questions";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("SurveyQuestions").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost("", CreateSurveyQuestion).DisableAntiforgery()
            .WithSummary("1 - Multiple Choice == 2 - Single Choice == 3 - Text\n ");
    }

    private static async Task<IResult> CreateSurveyQuestion(ISender sender,
        [FromBody] Commands.SurveyQuestionCommandWithSurveyIdCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}