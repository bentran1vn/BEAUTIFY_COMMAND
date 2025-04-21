using BEAUTIFY_COMMAND.CONTRACT.Services.ShiftConfigs;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.ShiftConfigs;

public class ShiftConfigApi: ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/shiftConfigs";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Shift Configs")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost("", () => { })
            .RequireAuthorization(Constant.Role.CLINIC_ADMIN);

        gr1.MapPut("", () => { })
            .RequireAuthorization(Constant.Role.CLINIC_ADMIN);
    }
    
    private static async Task<IResult> CreateShiftConfig(
        ISender sender, HttpContext httpContext,
        [FromBody] Commands.CreateShiftConfigBody command)
    {
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value!;
        
        var result = await sender.Send(new Commands.CreateShiftConfigCommand(command.Name,
            command.Note, command.StartTime, command.EndTime, new Guid(clinicId)));
        
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> UpdateShiftConfig(
        ISender sender, HttpContext httpContext,
        [FromBody] Commands.UpdateShiftConfigBody command)
    {
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value!;
        
        var result = await sender.Send(new Commands.UpdateShiftConfigCommand(command.Id ,command.Name,
            command.Note, command.StartTime, command.EndTime, new Guid(clinicId)));
        
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}