using BEAUTIFY_COMMAND.CONTRACT.Services.UserClinics;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.UserClinics;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/user-clinics";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("UserClinics")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPatch("doctors/{doctorId:guid}/branch", ChangeDoctorBranch);
    }

    private static async Task<IResult> ChangeDoctorBranch(
        [FromServices] ISender sender,
        Guid doctorId,
        [FromBody] ChangeBranchRequest request)
    {
        var result =
            await sender.Send(
                new Commands.ChangeDoctorToAnotherBranchCommand(request.OldBranchId, request.NewBranchId, doctorId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private class ChangeBranchRequest
    {
        public Guid NewBranchId { get; set; }
        public Guid OldBranchId { get; set; }
    }
}