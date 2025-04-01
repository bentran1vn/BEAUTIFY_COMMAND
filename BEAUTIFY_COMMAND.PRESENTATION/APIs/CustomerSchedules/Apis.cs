using BEAUTIFY_COMMAND.CONTRACT.Services.CustomerSchedule;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.CustomerSchedules;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "api/v{verison:apiVersion}/customer-schedules";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("CustomerSchedules").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPatch("{scheduleId:guid}/{status}", UpdateCustomerScheduleAfterPaymentCompleted);
        gr1.MapPatch("staff/{scheduleId:guid}/{status}",
            StaffUpdateCustomerScheduleStatusAfterCheckIn).RequireAuthorization();
        gr1.MapPost("generate/{customerScheduleId:guid}", GenerateCustomerScheduleAfterPayment);
    }

    private static async Task<IResult> UpdateCustomerScheduleAfterPaymentCompleted(ISender sender,
        Guid scheduleId, string status)
    {
        var result = await sender.Send(
            new Command.UpdateCustomerScheduleAfterPaymentCompletedCommand(scheduleId, status));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> StaffUpdateCustomerScheduleStatusAfterCheckIn(ISender sender,
        Guid scheduleId, string status)
    {
        var result = await sender.Send(
            new Command.StaffUpdateCustomerScheduleStatusAfterCheckInCommand(scheduleId, status));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> GenerateCustomerScheduleAfterPayment(ISender sender, Guid customerScheduleId)
    {
        var result = await sender.Send(
            new Command.GenerateCustomerScheduleAfterPaymentCompletedCommand(customerScheduleId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}