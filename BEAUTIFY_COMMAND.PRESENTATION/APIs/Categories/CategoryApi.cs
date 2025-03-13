using BEAUTIFY_COMMAND.CONTRACT.Services.Categories;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Shared;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Categories;
public class CategoryApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/categories";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Categories")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost(string.Empty, CreateCatergory)
            .WithName("Create Catorgory Information")
            .WithSummary("Create Catorgory Information.")
            .WithDescription("");

        gr1.MapPut("{id}", UpdateCatergory)
            .WithName("Update Catorgory Information")
            .WithSummary("Update Catorgory Information.")
            .WithDescription("");

        gr1.MapDelete("{id}", DeleteCatergory)
            .WithName("Delete Catorgory")
            .WithSummary("Delete Catorgory")
            .WithDescription("");
        gr1.MapPost("{SubCategoryId}/move-to/{CategoryId}", MoveSubCategoryToCategoryCommand)
            .WithName("Move SubCategory To Category")
            .WithSummary("Move SubCategory To Category")
            .WithDescription("");
    }

    private static async Task<IResult> CreateCatergory(ISender sender,
        [FromBody] Commands.CreateCategoryCommand command)
    {
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateCatergory(ISender sender, Guid id,
        [FromBody] Commands.UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));
        }

        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> DeleteCatergory(ISender sender, Guid id,
        [FromBody] Commands.DeleteCategoryCommand command)
    {
        if (id != command.Id)
        {
            return HandlerFailure(Result.Failure(new Error("400", "Id mismatch.")));
        }

        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> MoveSubCategoryToCategoryCommand(ISender sender, Guid SubCategoryId,
        Guid CategoryId)
    {
        var result = await sender.Send(new Commands.MoveSubCategoryToCategoryCommand(SubCategoryId, CategoryId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}