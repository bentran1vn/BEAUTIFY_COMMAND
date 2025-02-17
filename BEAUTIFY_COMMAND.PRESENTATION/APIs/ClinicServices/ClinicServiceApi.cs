using BEAUTIFY_COMMAND.CONTRACT.Services.ClinicSerivices;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Shared;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.ClinicServices;

public class ClinicServiceApi: ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/clinicServices";
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("ClinicServices")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost(string.Empty, CreateClinicServices)
            .DisableAntiforgery()
            .WithName("Create Clinic's Service")
            .WithSummary("Create Clinic's Service.")
            .WithDescription("");
        
        gr1.MapDelete("{id}", DeleteClinicServices)
            .WithName("Delete Clinic's Service")
            .WithSummary("Delete Clinic's Service")
            .WithDescription("");
    }
    
    private static async Task<IResult> CreateClinicServices(ISender sender,
        [FromForm] Commands.CreateClinicServiceCommand command)
    {
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> DeleteClinicServices(ISender sender, Guid id,
        [FromBody] Commands.DeleteClinicServiceCommand command)
    {
        if (id != command.Id)
        {
            return  HandlerFailure(Result.Failure(new Error("500", "Id mismatch.")));
        }
        
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}