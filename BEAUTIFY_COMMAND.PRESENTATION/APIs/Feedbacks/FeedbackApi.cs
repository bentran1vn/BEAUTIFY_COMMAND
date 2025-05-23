using BEAUTIFY_COMMAND.CONTRACT.Services.Feedbacks;
using Newtonsoft.Json;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Feedbacks;
public class FeedbackApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/feedbacks";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Feedbacks")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost("", CreateFeedback)
            .DisableAntiforgery()
            .RequireAuthorization();
        gr1.MapPut("", UpdatedFeedback)
            .DisableAntiforgery()
            .RequireAuthorization();
        gr1.MapPost("Display", DisplayFeedback).RequireAuthorization();
    }

    private static async Task<IResult> CreateFeedback(ISender sender,
        [FromForm] Commands.CreateFeedbackBody command)
    {
        var listSchedule = command.ScheduleFeedbacks;
        var schedules = JsonConvert.DeserializeObject<List<Commands.ScheduleFeedback>>(listSchedule);

        if (schedules == null || !schedules.Any()) throw new Exception("Empty schedule feedbacks");

        var commandBody = new Commands.CreateFeedbackCommand
        {
            OrderId = command.OrderId,
            Images = command.Images,
            Content = command.Content,
            Rating = command.Rating,
            ScheduleFeedbacks = schedules
        };

        var result = await sender.Send(commandBody);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdatedFeedback(ISender sender,
        [FromForm] Commands.UpdateFeedbackBody command)
    {
        var listSchedule = command.ScheduleFeedbacks;
        var schedules = JsonConvert.DeserializeObject<List<Commands.ScheduleFeedback>>(listSchedule);

        if (schedules == null || !schedules.Any()) throw new Exception("Empty schedule feedbacks");

        var updateCommand = new Commands.UpdateFeedbackCommand
        {
            FeedbackId = command.FeedbackId,
            Images = command.Images,
            Content = command.Content,
            Rating = command.Rating,
            ScheduleFeedbacks = schedules
        };

        var result = await sender.Send(updateCommand);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DisplayFeedback(ISender sender,
        [FromBody] Commands.ViewFeedbackCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}