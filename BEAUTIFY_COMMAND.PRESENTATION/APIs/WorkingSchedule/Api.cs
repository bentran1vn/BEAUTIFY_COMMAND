using BEAUTIFY_COMMAND.CONTRACT.Services.WorkingSchedules;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.WorkingSchedule;
public class Api : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/clinics/{clinicId}/schedules";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Working Schedules")
            .MapGroup(BaseUrl)
            .HasApiVersion(1);

        gr1.MapPost("", CreateClinicEmptySchedule)
            .RequireAuthorization(Constant.Role.CLINIC_STAFF)
            .WithDescription("Initialize empty working schedules for a clinic");
    }

    private static async Task<IResult> CreateWorkingSchedule(ISender sender,
        [FromBody] Commands.CreateWorkingScheduleCommand createWorkingScheduleCommand)
    {
        var result = await sender.Send(createWorkingScheduleCommand);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CreateClinicEmptySchedule(ISender sender,
        [FromBody] Commands.CreateClinicEmptyScheduleCommand createClinicEmptyScheduleCommand)
    {
        var result = await sender.Send(createClinicEmptyScheduleCommand);
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