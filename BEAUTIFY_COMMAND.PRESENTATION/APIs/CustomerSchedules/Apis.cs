using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.CustomerSchedules;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "api/v{verison:apiVersion}/customer-schedules";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("CustomerSchedules").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPatch("{scheduleId:guid}/{status}", UpdateCustomerScheduleAfterPaymentCompleted);
    }

    private static async Task<IResult> UpdateCustomerScheduleAfterPaymentCompleted(ISender sender,
        Guid scheduleId, string status)
    {
        var result = await sender.Send(
            new Command.UpdateCustomerScheduleAfterPaymentCompletedCommand(scheduleId, status));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}