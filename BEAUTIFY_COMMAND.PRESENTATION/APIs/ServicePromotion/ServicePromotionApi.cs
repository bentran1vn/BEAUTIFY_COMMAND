using BEAUTIFY_COMMAND.CONTRACT.Services.ServicePromotions;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Shared;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.ServicePromotion;
public class ServicePromotionApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/servicePromotions";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("ServicePromotions")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost(string.Empty, CreatePromotionServices)
            .DisableAntiforgery()
            .WithName("Create Clinic's Service Promotion")
            .WithSummary("Create Clinic's Service Promotion.")
            .WithDescription("")
            .RequireAuthorization();

        gr1.MapPut("{id}", UpdatePromotionServices)
            .WithName("Update Clinic's Service Promotion")
            .WithSummary("Update Clinic's Service Promotion")
            .WithDescription("")
            .DisableAntiforgery()
            .RequireAuthorization();

        gr1.MapDelete("{id}", DeletePromotionServices)
            .WithName("Delete Clinic's Service Promotion")
            .WithSummary("Delete Clinic's Service Promotion")
            .WithDescription("")
            .RequireAuthorization();
    }

    private static async Task<IResult> CreatePromotionServices(ISender sender
        , HttpContext httpContext, [FromForm] Commands.CreatePromotionServicesBody command)
    {
        var userId = httpContext.User.FindFirst(c => c.Type == "UserId")?.Value!;
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value!;

        var result = await sender.Send(new Commands.CreatePromotionServicesCommand(
            Guid.Parse(userId),
            Guid.Parse(clinicId),
            command.ServiceId,
            command.Name,
            command.DiscountPercent,
            command.Image,
            command.StartDay,
            command.EndDate
        ));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdatePromotionServices(ISender sender, Guid id,
        HttpContext httpContext, [FromForm] Commands.UpdatePromotionServicesBody command)
    {
        if (id != command.PromotionId) return HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));
        
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value!;

        var result = await sender.Send(new Commands.UpdatePromotionServicesCommand(
            Guid.Parse(clinicId),
            command.PromotionId,
            command.Name,
            command.DiscountPercent,
            command.Image,
            command.StartDay,
            command.EndDate,
            command.IsActivated));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeletePromotionServices(ISender sender, Guid id,
        HttpContext httpContext, [FromBody] Commands.DeletePromotionServicesBody command)
    {
        if (id != command.PromotionId) return HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));
        
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value!;
        
        var result = await sender.Send(new Commands.DeletePromotionServicesCommand(
            Guid.Parse(clinicId),
            command.PromotionId));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}