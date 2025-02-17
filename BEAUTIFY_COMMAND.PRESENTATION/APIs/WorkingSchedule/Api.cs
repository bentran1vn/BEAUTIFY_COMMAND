using BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.WorkingSchedule;
public class Api : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/working-schedules";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Working Schedules").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost("", CreateWorkingSchedule);
        gr1.MapDelete("{id:guid}", RemoveWorkingSchedule);
        gr1.MapPut("", UpdateWorkingSchedule);
    }

    private static async Task<IResult> CreateWorkingSchedule(ISender sender,
        [FromBody] Commands.CreateWorkingScheduleCommand createWorkingScheduleCommand)
    {
        var result = await sender.Send(createWorkingScheduleCommand);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> RemoveWorkingSchedule(ISender sender,
        Guid id)
    {
        var result = await sender.Send(new Commands.DeleteWorkingScheduleCommand(id));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateWorkingSchedule(ISender sender,
        [FromBody] Commands.UpdateWorkingScheduleCommand updateWorkingScheduleCommand)
    {
        var result = await sender.Send(updateWorkingScheduleCommand);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}