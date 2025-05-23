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

        gr1.MapPut("{id:guid}", UpdateClinic)
            .DisableAntiforgery()
            .WithName("UpdateClinic")
            .WithSummary("Update clinic information")
            .RequireAuthorization();

        gr1.MapPost("{id:guid}/accounts", ClinicCreateAccountForEmployee)
            .DisableAntiforgery()
            .WithSummary("1 - Doctor, 2 - Clinic Staff")
            .RequireAuthorization();

        gr1.MapDelete("{id:guid}/accounts/{userId:guid}", ClinicRemoveAccountForEmployee)
            .RequireAuthorization();

        gr1.MapPut("{id:guid}/accounts", ClinicUpdateAccountOfEmployeeCommand)
            .DisableAntiforgery()
            .RequireAuthorization();

        gr1.MapPatch("{id:guid}/status", ClinicUpdateStatus)
            .DisableAntiforgery()
            .RequireAuthorization("Clinic Admin and Staff");

        gr1.MapPatch("{id:guid}/staff/doctor", StaffChangeDoctorWorkingClinic)
            .DisableAntiforgery()
            .RequireAuthorization();

        gr1.MapPost("{id:guid}/branches", ClinicCreateBranch)
            .DisableAntiforgery()
            .RequireAuthorization();
        
        gr1.MapPut("{id}/response/branch", ResponseClinicBranchApply)
            .WithName("RespondToBranchApplyRequest")
            .WithSummary("Admin response to clinic branch create request")
            .WithDescription(
                "Action = 0 (Approve), Action = 1 (Reject).  Reject reason is required for Action 1.")
            .RequireAuthorization();

        gr1.MapPut("{id:guid}/branches/{branchId:guid}", ClinicUpdateBranch)
            .DisableAntiforgery()
            .RequireAuthorization();

        gr1.MapDelete("{id:guid}/branches/{branchId:guid}", ClinicDeleteBranch)
            .DisableAntiforgery()
            .RequireAuthorization();
    }

    private static async Task<IResult> ClinicUpdateStatus(ISender sender, [FromRoute] Guid id)
    {
        var result = await sender.Send(new Commands.ChangeClinicActivateStatusCommand(id));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> StaffChangeDoctorWorkingClinic(ISender sender,
        [FromForm] Commands.StaffChangeDoctorWorkingClinicCommand command, Guid id)
    {
        var result = await sender.Send(command with { ClinicId = id });

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicUpdateAccountOfEmployeeCommand(ISender sender,
        [FromForm] Commands.ClinicUpdateAccountOfEmployeeCommand command)
    {
        var result = await sender.Send(command);

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicCreateAccountForEmployee(ISender sender, [FromRoute] Guid id,
        [FromForm] Commands.ClinicCreateAccountForEmployeeCommand command)
    {
        if (id != command.ClinicId) throw new Exception("Id not matching");

        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicRemoveAccountForEmployee(ISender sender, [FromRoute] Guid id,
        [FromRoute] Guid userId)
    {
        var result = await sender.Send(new Commands.ClinicDeleteAccountOfEmployeeCommand(id, userId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }


    private static async Task<IResult> ClinicApply(ISender sender, HttpContext httpContext,
        [FromForm] Commands.ClinicApplyBody command)
    {   
        var clinicId = httpContext.User.FindFirst(c => c.Type == "ClinicId")?.Value;
        var roleName = httpContext.User.FindFirst(c => c.Type == "RoleName")?.Value;
        var result = await sender.Send(new Commands.ClinicApplyCommand(clinicId == null ? null : new Guid(clinicId), roleName,
            command.Name, command.Email, command.PhoneNumber, command.City, command.District,
            command.Ward,command.Address, command.TaxCode, command.BankName, command.BankAccountNumber,
            command.BusinessLicense, command.OperatingLicense, command.OperatingLicenseExpiryDate,
            command.ProfilePictureUrl));

        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> UpdateClinic(ISender sender, [FromRoute] Guid id,
        [FromForm] Commands.UpdateClinicCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ResponseClinicApply(ISender sender, [FromRoute] string id,
        Commands.ResponseClinicApplyCommand command)
    {
        var result = await sender.Send(command with { RequestId = id });
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
    
    private static async Task<IResult> ResponseClinicBranchApply(ISender sender, [FromRoute] string id,
        Commands.ResponseClinicBranchApplyCommand command)
    {
        var result = await sender.Send(command with { RequestId = id });
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicCreateBranch(ISender sender, [FromRoute] Guid id,
        [FromForm] Commands.ClinicCreateBranchCommand command)
    {
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicUpdateBranch(ISender sender, [FromRoute] Guid id,
        [FromRoute] Guid branchId, [FromForm] Commands.ClinicUpdateBranchCommand command)
    {
        command.BranchId = branchId;
        var result = await sender.Send(command);
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }

    private static async Task<IResult> ClinicDeleteBranch(ISender sender, [FromRoute] Guid id,
        [FromRoute] Guid branchId, [FromForm] Commands.ClinicDeleteBranchCommand command)
    {
        var result = await sender.Send(new Commands.ClinicDeleteBranchCommand(branchId));
        return result.IsFailure ? HandlerFailure(result) : Results.Ok(result);
    }
}