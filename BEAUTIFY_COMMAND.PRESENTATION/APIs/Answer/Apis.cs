using BEAUTIFY_COMMAND.CONTRACT.Services.Answer;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Answer;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/survey-answers";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("SurveyAnswers").MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost("", CreateSurveyAnswer).RequireAuthorization().DisableAntiforgery()
            .WithSummary("Answer Survey Questions");
        gr1.MapPost("determine-answer", DetermineSurveyAnswer).RequireAuthorization().DisableAntiforgery();
    }

    private static async Task<IResult> CreateSurveyAnswer(ISender sender,
        [FromBody] Commands.CustomerAnswerSurveyCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DetermineSurveyAnswer(ISender sender,
        [FromBody] Commands.DetermineSurveyAnswerCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}