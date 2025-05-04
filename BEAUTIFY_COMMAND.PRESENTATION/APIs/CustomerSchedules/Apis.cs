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
            StaffUpdateCustomerScheduleStatusAfterCheckIn).RequireAuthorization(Constant.Role.CLINIC_STAFF);
        gr1.MapPost("generate/{customerScheduleId:guid}", GenerateCustomerScheduleAfterPayment)
            .RequireAuthorization(Constant.Role.CLINIC_STAFF);
        gr1.MapPatch("doctor/{customerScheduleId:guid}/", DoctorUpdateCustomerScheduleNote)
            .RequireAuthorization(Constant.Role.DOCTOR);
        ;
        gr1.MapPut("customer/{customerScheduleId:guid}/", CustomerRequestSchedule);
        gr1.MapPatch("staff/{customerScheduleId:guid}/", StaffUpdateCustomerScheduleTimeCommand)
            .RequireAuthorization(Constant.Role.CLINIC_STAFF);
        gr1.MapPatch("staff/approve/{customerScheduleId:guid}/", StaffUpdateCustomerScheduleStatusAfterCustomerRequest)
            .RequireAuthorization(Constant.Role.CLINIC_STAFF);
        gr1.MapPatch("staff/{customerScheduleId:guid}/cancellation", StaffCancelCustomerScheduleAfterFirstStep)
            .RequireAuthorization(Constant.Role.CLINIC_STAFF);
    }

    private static async Task<IResult> StaffCancelCustomerScheduleAfterFirstStep(
        ISender sender,
        Guid customerScheduleId, Command.StaffCancelCustomerScheduleAfterFirstStepCommand command)
    {
        var result =
            await sender.Send(
                new Command.StaffCancelCustomerScheduleAfterFirstStepCommand(CustomerScheduleId: customerScheduleId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> StaffUpdateCustomerScheduleStatusAfterCustomerRequest(
        ISender sender,
        Guid customerScheduleId, Command.StaffApproveCustomerScheduleCommand command)
    {
        var result = await sender.Send(command with { CustomerScheduleId = customerScheduleId });
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> StaffUpdateCustomerScheduleTimeCommand(
        ISender sender,
        Guid customerScheduleId, Command.StaffUpdateCustomerScheduleTimeCommand command)
    {
        var result = await sender.Send(command with { CustomerScheduleId = customerScheduleId });
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
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

    private static async Task<IResult> DoctorUpdateCustomerScheduleNote(ISender sender,
        Guid customerScheduleId, [FromBody] string doctorNote)
    {
        var result = await sender.Send(
            new Command.DoctorUpdateCustomerScheduleNoteCommand(customerScheduleId, doctorNote));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> CustomerRequestSchedule(ISender sender,
        Guid customerScheduleId, Command.CustomerRequestScheduleCommand command)
    {
        var result = await sender.Send(command with { CustomerScheduleId = customerScheduleId });
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}