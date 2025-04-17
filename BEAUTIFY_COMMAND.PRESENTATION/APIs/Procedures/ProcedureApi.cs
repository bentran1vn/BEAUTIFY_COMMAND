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

        gr1.MapPost(string.Empty, CreateProcedure);

        gr1.MapPut("{procedureId}", UpdateProcedure);

        gr1.MapDelete("{id}", DeleteProcedure)
            .WithName("Delete Service's Procedures")
            .WithSummary("Delete Service's Procedures")
            .WithDescription("");
    }

    private static async Task<IResult> CreateProcedure(ISender sender,
        [FromBody] Commands.CreateProcedureCommand command)
    {
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateProcedure(ISender sender,
        [FromBody] Commands.UpdateProcedureCommand command, Guid procedureId)
    {
        if (procedureId != command.ProcedureId)
            return HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));

        var result = await sender.Send(command);

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