using BEAUTIFY_COMMAND.CONTRACT.Services.DoctorCertificate;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.DoctorCertificate;
public class DoctorCertificateEndpoint : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/doctor-certificates";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Doctor Certificates").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost(string.Empty, CreateDoctorCertificate).DisableAntiforgery();
        gr1.MapPut("{id:guid}", UpdateDoctorCertificate);
        gr1.MapDelete("{id:guid}", DeleteDoctorCertificate);
    }

    private static async Task<IResult> CreateDoctorCertificate(
        ISender sender,
        [FromForm] Commands.CreateDoctorCertificateCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateDoctorCertificate(
        ISender sender,
        Guid id,
        [FromForm] Commands.UpdateCommand command)
    {
        command.Id = id;
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeleteDoctorCertificate(
        ISender sender,
        Guid id)
    {
        var result = await sender.Send(new Commands.DeleteCommand(id));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}