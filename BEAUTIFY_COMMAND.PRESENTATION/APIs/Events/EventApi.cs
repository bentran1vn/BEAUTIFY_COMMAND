using BEAUTIFY_COMMAND.CONTRACT.Services.Events;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Events;

public class EventApi: ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/events";
    private const string Base1Url = "/api/v{version:apiVersion}/followers";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Events")
            .MapGroup(BaseUrl).HasApiVersion(1);
        
        gr1.MapPost("", CreateEvent)
            .DisableAntiforgery()
            .RequireAuthorization(Constant.Role.CLINIC_ADMIN);

        gr1.MapPut("{id}", UpdateEvent)
            .DisableAntiforgery()
            .RequireAuthorization(Constant.Role.CLINIC_ADMIN);
        
        gr1.MapDelete("{id}", DeleteEvent)
            .RequireAuthorization(Constant.Role.CLINIC_ADMIN);
        
        var gr2 = app.NewVersionedApi("Followers")
            .MapGroup(Base1Url).HasApiVersion(1);
        
        gr2.MapPost("", Following)
            .RequireAuthorization(Constant.Role.CUSTOMER);
    }
    
    private static async Task<IResult> Following(
        ISender sender, HttpContext httpContext,
        [FromForm] CONTRACT.Services.Followers.Commands.FollowBody command)
    {
        var userId = httpContext.User.FindFirst(c => c.Type == "UserId")?.Value!;
        
        var result = await sender.Send(new CONTRACT.Services.Followers.Commands.FollowCommand(command, new Guid(userId)));
        
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> CreateEvent(
        ISender sender, HttpContext httpContext,
        [FromForm] Commands.EventBody command)
    {
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value!;
        
        var result = await sender.Send(new Commands.CreateEventCommand(command, new Guid(clinicId)));
        
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> UpdateEvent(
        ISender sender, HttpContext httpContext,
        [FromForm] Commands.EventBody command, Guid id)
    {
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value!;
        
        var result = await sender.Send(new Commands.UpdateEventCommand(command, id, new Guid(clinicId)));
        
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> DeleteEvent(
        ISender sender, HttpContext httpContext, Guid id)
    {
        var result = await sender.Send(new Commands.DeleteEventCommand(id));
        
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}