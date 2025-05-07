using BEAUTIFY_COMMAND.CONTRACT.Services.Configs;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Configs;
public class Apis : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "api/v{verison:apiVersion}/configs";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Configs").MapGroup(BaseUrl).HasApiVersion(1);
        gr1.MapPost("", CreateConfigCommand)
            .RequireAuthorization(Constant.Role.SYSTEM_ADMIN);
        gr1.MapPut("{configId:guid}", UpdateConfigCommand)
            .RequireAuthorization(Constant.Role.SYSTEM_ADMIN);
    }

    private static async Task<IResult> CreateConfigCommand(
        ISender sender,
        Command.CreateConfigCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateConfigCommand(
        ISender sender,
        Guid configId, Command.UpdateConfigCommand command)
    {
        var result = await sender.Send(command with { Id = configId });
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}