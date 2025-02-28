using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Abstractions.Messages;
using Microsoft.AspNetCore.Http;


namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics;
public static class Commands
{
    #region ClinicApplyCommand

    public record ClinicApplyCommand(
        string Name,
        string Email,
        string PhoneNumber,
        string Address,
        string TaxCode,
        IFormFile BusinessLicense,
        IFormFile OperatingLicense,
        string OperatingLicenseExpiryDate,
        IFormFile ProfilePictureUrl
    ) : ICommand;

    #endregion

    #region UpdateClinicCommand

    public class UpdateClinicCommand : ICommand
    {
        public string ClinicId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public bool? IsActivated { get; set; }
    }

    #endregion

    #region ResponseClinicApplyCommand

    public record ResponseClinicApplyCommand(
        string RequestId,
        string? RejectReason,
        int Action
        // 0 Approve, 1 Reject, 2 Banned
    ) : ICommand;

    #endregion


    #region ClinicCreateAccountForEmployeeCommand

    public record ClinicCreateAccountForEmployeeCommand(
        Guid ClinicId,
        string Email,
        string Password,
        string FirstName,
        string LastName,
        Guid RoleId
    ) : ICommand;

    #endregion

    #region ClinicDeleteAccountOfEmployeeCommand

    public record ClinicDeleteAccountOfEmployeeCommand(
        Guid ClinicId,
        Guid UserId
    ) : ICommand;

    #endregion

    #region ClinicUpdateAccountOfEmployeeCommand

    public class ClinicUpdateAccountOfEmployeeCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }

    #endregion

    #region StaffChangeDoctorWorkingClinicCommand

    public record StaffChangeDoctorWorkingClinicCommand(
        Guid ClinicId,
        Guid DoctorId
    ) : ICommand;

    #endregion
    
    #region ClinicCreateBranchCommand

    public record ClinicCreateBranchCommand(
        string Name,
        string Email,
        string PhoneNumber,
        string Address,
        IFormFile OperatingLicense,
        DateTimeOffset OperatingLicenseExpiryDate,
        IFormFile ProfilePictureUrl
        ) : ICommand;

    #endregion

    #region ClinicUpdateBranchCommand

    public record ClinicUpdateBranchCommand(
        Guid BranchId,
        string Name,
        string PhoneNumber,
        string Address,
        IFormFile? ProfilePicture,
        bool IsActivated
    ) : ICommand;

    #endregion

    #region ClinicDeleteBranchCommand

    public record ClinicDeleteBranchCommand(
        Guid BranchId
    ) : ICommand;

    #endregion
    
    
    public record TestTokenCommand : ICommand;
}