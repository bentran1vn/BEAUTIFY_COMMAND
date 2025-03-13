using System.Text.Json;
using BEAUTIFY_COMMAND.CONTRACT.Services.Procedures;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Shared;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Procedures;
public class ProcedureApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/procedures";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Procedures")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost(string.Empty, CreateProcedure)
            .DisableAntiforgery()
            .WithName("Create Service's Procedures")
            .WithSummary("Create Service's Procedures.")
            .WithDescription("1 Service has may Procedures, and one procedure may have may way to collect money " +
                             "( Ví dụ bước vệ sinh da: Default: 20k, Tẩy tế bào chết: 50k, ... )." +
                             "ProcedurePriceTypes please input Json String (" +
                             "Example: [{\"Name\":\"ABBC\",\"Duration\":60,\"Price\":1000},{\"Name\":\"BCCCC\",\"Duration\":90,\"Price\":1500}] )");

        gr1.MapDelete("{id}", DeleteProcedure)
            .WithName("Delete Service's Procedures")
            .WithSummary("Delete Service's Procedures")
            .WithDescription("");
    }

    private static async Task<IResult> CreateProcedure(ISender sender,
        [FromForm] Commands.CreateProcedureBody command)
    {
        List<Commands.ProcedurePriceType>? priceTypes = null;
        if (!string.IsNullOrWhiteSpace(command.ProcedurePriceTypes))
            priceTypes = JsonSerializer.Deserialize<List<Commands.ProcedurePriceType>>(
                command.ProcedurePriceTypes
            );


        var result = await sender.Send(new Commands.CreateProcedureCommand(
            command.ClinicServiceId, command.Name, command.Description, command.StepIndex,
            command.ProcedureCoverImage, priceTypes)
        );

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeleteProcedure(ISender sender, Guid id,
        [FromBody] Commands.DeleteProcedureCommand command)
    {
        if (id != command.Id) return HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));

        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}