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

        gr1.MapPost("", ClinicApply)
            .DisableAntiforgery()
            .WithName("ApplyClinic")
            .WithSummary("Create a new clinic request")
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
                                    ["name"] = new()
                                        { Type = "string", Example = new OpenApiString("Thẩm mĩ viện Hướng Dương") },
                                    ["email"] = new()
                                    {
                                        Type = "string", Format = "email",
                                        Example = new OpenApiString("tan11105@gmail.com")
                                    },
                                    ["phone_number"] = new()
                                    {
                                        Type = "string", Format = "phone", Example = new OpenApiString("+84983460123")
                                    },
                                    ["address"] = new()
                                        { Type = "string", Example = new OpenApiString("Biên Hoà, Đồng Nai") },
                                    ["tax_code"] = new() { Type = "string", Example = new OpenApiString("123123") },
                                    ["business_license"] = new() { Type = "string", Format = "binary" },
                                    ["operating_license"] = new() { Type = "string", Format = "binary" },
                                    ["operating_license_expiry_date"] = new()
                                        { Type = "string", Format = "date", Example = new OpenApiString("2025-12-31") },
                                    ["profile_picture_url"] = new() { Type = "string", Format = "binary" }
                                }
                            }
                        }
                    }
                }
            });

        gr1.MapPut("{id}/response", ResponseClinicApply)
            .WithName("RespondToApplyRequest")
            .WithSummary("Admin response to clinic request")
            .WithDescription(
                "Action = 0 (Approve), Action = 1 (Reject), Action = 2 (Ban). Reject reason is required for Action 1 and 2.")
            .RequireAuthorization();

        gr1.MapPut("{id}", UpdateClinic)
            .DisableAntiforgery()
            .WithName("UpdateClinic")
            .WithSummary("Update clinic information")
            .RequireAuthorization();

        gr1.MapPost("{id}/accounts", ClinicCreateAccountForEmployee)
            .RequireAuthorization();
        gr1.MapDelete("{id}/accounts", ClinicRemoveAccountForEmployee)
            .RequireAuthorization();
        gr1.MapPut("{id}/accounts", ClinicUpdateAccountOfEmployeeCommand)
            .DisableAntiforgery()
            .RequireAuthorization();
        gr1.MapPatch("{id}/status", ClinicUpdateStatus)
            .DisableAntiforgery()
            .RequireAuthorization();
        gr1.MapPatch("{id}/staff/doctor", StaffChangeDoctorWorkingClinic)
            .DisableAntiforgery()
            .RequireAuthorization();
        gr1.MapPost("{id}/branches", ClinicCreateBranch)
            .DisableAntiforgery()
            .RequireAuthorization();

        gr1.MapPut("{id}/branches/{branchId}", ClinicUpdateBranch)
            .DisableAntiforgery()
            .RequireAuthorization();
        gr1.MapDelete("{id}/branches/{branchId}", ClinicDeleteBranch)
            .DisableAntiforgery()
            .RequireAuthorization();
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

    private static async Task<IResult> ClinicUpdateStatus(ISender sender, Guid clinicId)
    {
        var result = await sender.Send(new Commands.ChangeClinicActivateStatusCommand(clinicId));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}