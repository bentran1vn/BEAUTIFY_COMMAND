using BEAUTIFY_COMMAND.CONTRACT.Services.DoctorServices;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.DoctorServices;
public class Api : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/doctor-services";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Doctor Services").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost(string.Empty, CreateDoctorService);
     //   gr1.MapDelete(string.Empty, DeleteDoctorService);
    }


    private static async Task<IResult> CreateDoctorService(ISender sender,
        [FromBody] Commands.DoctorSetWorkingServiceCommand createDoctorServiceCommand)
    {
        var result = await sender.Send(createDoctorServiceCommand);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeleteDoctorService(ISender sender,
        [FromBody] Commands.DeleteDoctorServiceCommand deleteDoctorServiceCommand)
    {
        var result = await sender.Send(deleteDoctorServiceCommand);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}