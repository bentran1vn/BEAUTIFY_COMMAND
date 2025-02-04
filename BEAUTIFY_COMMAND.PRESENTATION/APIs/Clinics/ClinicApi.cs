using System.Text.Json;
using BEAUTIFY_COMMAND.CONTRACT.Services.Clinics;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Clinics;
public class ClinicApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/clinics";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Clinics")
            .MapGroup(BaseUrl).HasApiVersion(1);
        
        gr1.MapPost("apply", ClinicApply)
            .WithName("apply")
            .WithSummary("New Clinic send a request to Join System.")
            .WithDescription("Clinics can send a request to join a clinic." +
                " Clinic can apply multiple times when clinic request was rejected (Each apply time is" +
                " 30 days apart). If Clinic is banned user can not send request again." +
                "A clinics when apply again must send email, phone number, tax code same with the last request.")
            .WithOpenApi(operation => new(operation)
                {
                    RequestBody = new OpenApiRequestBody()
                    {
                        Content =
                        {
                            ["application/json"] = new OpenApiMediaType
                            {
                                Example = new OpenApiString(JsonSerializer.Serialize(new Commands.ClinicApplyCommand(
                                    "Thẩm mĩ viện Hướng Dương",
                                    "tan11105@gmail.com",
                                    "+84983460123",
                                    "Biên Hoà, Đồng Nai",
                                    "123123",
                                    "https://www.facebook.com/bentran1vn/?locale=vi_VN",
                                    "https://www.facebook.com/bentran1vn/?locale=vi_VN",
                                    "2025-12-31",
                                    "https://www.facebook.com/bentran1vn/?locale=vi_VN"
                                )))
                            }
                        }
                    }
                }
            );
        gr1.MapPut("apply/{id}", ResponseClinicApply)
            .WithName("Response Apply Request")
            .WithSummary("Admin Response Apply Request.")
            .WithDescription("With Action = 0 is Approve, Action = 1 is Reject, Action = 2 is Banned." +
                             " With Action 1 and 2, reject reason must be included." +
                             " Id in the path with RequestId in the request body must be same.");
            // .RequireAuthorization()
    }
    
    private static async Task<IResult> ClinicApply(ISender sender, [FromBody] Commands.ClinicApplyCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }
    
    private static async Task<IResult> ResponseClinicApply(ISender sender, string id, [FromBody] Commands.ResponseClinicApplyCommand command)
    {
        if(command.RequestId != id) return Results.BadRequest();
        
        var result = await sender.Send(command);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }
}