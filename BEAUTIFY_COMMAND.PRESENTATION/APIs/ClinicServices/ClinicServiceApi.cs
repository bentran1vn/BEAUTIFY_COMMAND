using System.Text.Json;
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
        
        gr1.MapPut("{id}", UpdateClinicServices)
            .DisableAntiforgery()
            .WithName("Update Clinic's Service")
            .WithSummary("Update Clinic's Service.")
            .WithDescription("Update Clinic's Service." +
                             "With the clinicsId for update with array please input: \"[\\\"3c2bd874-58de-4321-b9bb-5c6f869c81f6\\\"]\"" +
                             "indexCoverImagesChange is the index you want to change image, example: 1, 2, 3" +
                             "please input: \"[1]\" and the indexDescriptionImagesChange is the same !")
            
            .RequireAuthorization();
        
        gr1.MapDelete("{id}", DeleteClinicServices)
            .WithName("Delete Clinic's Service")
            .WithSummary("Delete Clinic's Service")
            .WithDescription("");
    }
    
    private static async Task<IResult> CreateClinicServices(ISender sender,
        [FromForm] Commands.CreateClinicServiceBody command)
    {
        List<Guid>? clinicId = null;
        
        if (!string.IsNullOrWhiteSpace(command.ClinicId))
        {
            clinicId = (JsonSerializer.Deserialize<List<string>>(
                command.ClinicId
            ) ?? []).Select(x => new Guid(x)).ToList();
        }
        
        if (clinicId == null)
        {
            return  HandlerFailure(Result.Failure(new Error("404", "Empty Clinics")));
        }
        
        var result = await sender.Send(new Commands.CreateClinicServiceCommand(
            clinicId, command.Name, command.CoverImages, command.Description,
            command.DescriptionImages, command.CategoryId
            ));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> UpdateClinicServices(ISender sender, Guid id,
        [FromForm] Commands.UpdateClinicServiceBody command, HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirst(c => c.Type == "UserId")?.Value!;
        
        if (id != command.Id)
        {
            return  HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));
        }
        
        List<Guid>? clinicId = null;
        
        if (!string.IsNullOrWhiteSpace(command.ClinicId))
        {
            clinicId = (JsonSerializer.Deserialize<List<string>>(
                command.ClinicId
            ) ?? []).Select(x => new Guid(x)).ToList();
        }
        
        if (clinicId == null)
        {
            return  HandlerFailure(Result.Failure(new Error("404", "Empty Clinics")));
        }

        List<int>? numbersIndexCoverImagesChange = null;
        
        if (!string.IsNullOrWhiteSpace(command.IndexCoverImagesChange))
        {
            numbersIndexCoverImagesChange = JsonSerializer.Deserialize<List<int>>(
                command.IndexCoverImagesChange
            );
        }

        List<int>? numbersIndexDescriptionImagesChange = null;
        
        if (!string.IsNullOrWhiteSpace(command.IndexDescriptionImagesChange))
        {
            numbersIndexDescriptionImagesChange = JsonSerializer.Deserialize<List<int>>(
                command.IndexDescriptionImagesChange
            );
        }
        
        var result = await sender.Send(new Commands.UpdateClinicServiceCommand(
            command.Id, new Guid(userId), clinicId, command.Name, numbersIndexCoverImagesChange,
            command.CoverImages, command.Description, numbersIndexDescriptionImagesChange,
            command.DescriptionImages, command.CategoryId
            ));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> DeleteClinicServices(ISender sender, Guid id,
        [FromBody] Commands.DeleteClinicServiceCommand command)
    {
        if (id != command.Id)
        {
            return  HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));
        }
        
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}