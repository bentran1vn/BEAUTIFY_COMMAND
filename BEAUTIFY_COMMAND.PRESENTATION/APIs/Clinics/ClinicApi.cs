using BEAUTIFY_COMMAND.CONTRACT.Services.Clinics;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace BEAUTIFY_COMMAND.PRESENTATION.APIs.Clinics;
[IgnoreAntiforgeryToken]
public class ClinicApi : ApiEndpoint, ICarterModule
{
    private const string BaseUrl = "/api/v{version:apiVersion}/clinics";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var gr1 = app.NewVersionedApi("Clinics")
            .MapGroup(BaseUrl).HasApiVersion(1);

        gr1.MapPost("application", ClinicApply)
            .DisableAntiforgery()
            .WithName("apply")
            .WithSummary("New Clinic send a request to Join System.")
            .WithDescription("Clinics can send a request to join a clinic." +
                             " Clinic can apply multiple times when clinic request was rejected (Each apply time is" +
                             " 30 days apart). If Clinic is banned user can not send request again." +
                             "A clinics when apply again must send email, phone number, tax code same with the last request.")
            .WithOpenApi(operation => new OpenApiOperation(operation)
                {
                    RequestBody = new OpenApiRequestBody
                    {
                        Content =
                        {
                            ["multipart/form-data"] = new OpenApiMediaType
                            {
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        {
                                            "Name",
                                            new OpenApiSchema
                                            {
                                                Type = "string", Example = new OpenApiString("Thẩm mĩ viện Hướng Dương")
                                            }
                                        },
                                        {
                                            "Email",
                                            new OpenApiSchema
                                            {
                                                Type = "string", Format = "email",
                                                Example = new OpenApiString("tan11105@gmail.com")
                                            }
                                        },
                                        {
                                            "PhoneNumber",
                                            new OpenApiSchema
                                            {
                                                Type = "string", Format = "phone",
                                                Example = new OpenApiString("+84983460123")
                                            }
                                        },
                                        {
                                            "Address",
                                            new OpenApiSchema
                                                { Type = "string", Example = new OpenApiString("Biên Hoà, Đồng Nai") }
                                        },
                                        {
                                            "TaxCode",
                                            new OpenApiSchema { Type = "string", Example = new OpenApiString("123123") }
                                        },
                                        { "BusinessLicense", new OpenApiSchema { Type = "string", Format = "binary" } },
                                        {
                                            "OperatingLicense", new OpenApiSchema { Type = "string", Format = "binary" }
                                        },
                                        {
                                            "OperatingLicenseExpiryDate",
                                            new OpenApiSchema
                                            {
                                                Type = "string", Format = "date",
                                                Example = new OpenApiString("2025-12-31")
                                            }
                                        },
                                        {
                                            "ProfilePictureUrl",
                                            new OpenApiSchema { Type = "string", Format = "binary" }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            );

        gr1.MapPut("application/{id}", ResponseClinicApply)
            .WithName("Response Apply Request")
            .WithSummary("Admin Response Apply Request.")
            .WithDescription("With Action = 0 is Approve, Action = 1 is Reject, Action = 2 is Banned." +
                             " With Action 1 and 2, reject reason must be included." +
                             " Id in the path with RequestId in the request body must be same.");
        // .RequireAuthorization()

        gr1.MapPut("{id}", UpdateClinic)
            .DisableAntiforgery()
            .WithName("Update Clinic Information")
            .WithSummary("Update Clinic Information.")
            .WithDescription("")
            .RequireAuthorization();

        gr1.MapPost("clinic-account", ClinicCreateAccountForEmployee);
        gr1.MapDelete("clinic-account", ClinicRemoveAccountForEmployee);
        gr1.MapPut("clinic-account", ClinicUpdateAccountOfEmployeeCommand).DisableAntiforgery();
        gr1.MapPut("staff-change-doctor-working-clinic", StaffChangeDoctorWorkingClinic).DisableAntiforgery();
        gr1.MapPost("create-branch", ClinicCreateBranch).DisableAntiforgery().RequireAuthorization();
        gr1.MapPut("update-branch", ClinicUpdateBranch).DisableAntiforgery().RequireAuthorization();
        gr1.MapDelete("delete-branch", ClinicDeleteBranch).DisableAntiforgery().RequireAuthorization();
    }


    private static async Task<IResult> StaffChangeDoctorWorkingClinic(ISender sender,
        [FromForm] Commands.StaffChangeDoctorWorkingClinicCommand command)
    {
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicUpdateAccountOfEmployeeCommand(ISender sender,
        [FromForm] Commands.ClinicUpdateAccountOfEmployeeCommand command)
    {
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicCreateAccountForEmployee(ISender sender,
        [FromForm] Commands.ClinicCreateAccountForEmployeeCommand command)
    {
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicRemoveAccountForEmployee(ISender sender, Guid clinicId, Guid userId)
    {
        var result = await sender.Send(new Commands.ClinicDeleteAccountOfEmployeeCommand(clinicId, userId));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }


    private static async Task<IResult> ClinicApply(ISender sender, [FromForm] Commands.ClinicApplyCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> UpdateClinic(ISender sender, [FromForm] Commands.UpdateClinicCommand command)
    {
        var result = await sender.Send(command);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> ResponseClinicApply(ISender sender, string id,
        [FromBody] Commands.ResponseClinicApplyCommand command)
    {
        if (command.RequestId != id) return Results.BadRequest();

        var result = await sender.Send(command);

        if (result.IsFailure)
            return HandlerFailure(result);

        return Results.Ok(result);
    }

    private static async Task<IResult> ClinicCreateBranch(ISender sender,
        [FromForm] Commands.ClinicCreateBranchCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicUpdateBranch(ISender sender,
        [FromForm] Commands.ClinicUpdateBranchCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicDeleteBranch(ISender sender,
        [FromForm] Commands.ClinicDeleteBranchCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}