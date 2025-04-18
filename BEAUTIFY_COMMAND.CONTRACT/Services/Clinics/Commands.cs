using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BEAUTIFY_COMMAND.CONTRACT.Services.Clinics;
public static class Commands
{
    #region ClinicApplyCommand

    public record ClinicApplyCommand(
        Guid ClinicId,
        string RoleName,
        string Name,
        string Email,
        string PhoneNumber,
        string City,
        string District,
        string Ward,
        string Address,
        string TaxCode,
        string BankName,
        string BankAccountNumber,
        IFormFile BusinessLicense,
        IFormFile OperatingLicense,
        string OperatingLicenseExpiryDate,
        IFormFile ProfilePictureUrl
    ) : ICommand;

    public class ClinicApplyBody
    {
        public string Name {get; set;}
        public string Email {get; set;}
        public string PhoneNumber {get; set;}
        public string City {get; set;}
        public string District {get; set;}
        public string Ward {get; set;}
        public string Address {get; set;}
        public string TaxCode {get; set;}
        public string BankName {get; set;}
        public string BankAccountNumber {get; set;}
        public IFormFile BusinessLicense {get; set;}
        public IFormFile OperatingLicense {get; set;}
        public string OperatingLicenseExpiryDate {get; set;}
        public IFormFile ProfilePictureUrl {get; set;}
    }
    
    #endregion

    #region UpdateClinicCommand

    public class UpdateClinicCommand : ICommand
    {
        public string ClinicId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public TimeSpan? WorkingTimeStart { get; set; }
        public TimeSpan? WorkingTimeEnd { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public bool? IsActivated { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNumber { get; set; }
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
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
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

    public class ClinicCreateBranchCommand : ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public TimeSpan WorkingTimeStart { get; set; }
        public TimeSpan WorkingTimeEnd { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public IFormFile OperatingLicense { get; set; }
        public DateTimeOffset OperatingLicenseExpiryDate { get; set; }
        public IFormFile? ProfilePictureUrl { get; set; }
    }

    #endregion

    #region ClinicUpdateBranchCommand

    public class ClinicUpdateBranchCommand : ICommand
    {
        public Guid BranchId { get; set; }
        public string Name { get; set; }

        [MaxLength(15, ErrorMessage = "Phone Number must be 10 digits")]
        public string PhoneNumber { get; set; }

        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public TimeSpan WorkingTimeStart { get; set; }
        public TimeSpan WorkingTimeEnd { get; set; }
        public bool IsActivated { get; set; }
        public IFormFile? BusinessLicense { get; set; }
        public IFormFile? OperatingLicense { get; set; }
        public DateTimeOffset OperatingLicenseExpiryDate { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNumber { get; set; }
    }

    #endregion

    #region ClinicDeleteBranchCommand

    public record ClinicDeleteBranchCommand(
        Guid BranchId
    ) : ICommand;

    #endregion

    public record ChangeClinicActivateStatusCommand(
        Guid ClinicId
    ) : ICommand;

    #region ClinicCreateAccountForEmployeeCommand

    public record ClinicCreateAccountForEmployeeCommand(
        Guid ClinicId,
        string Email,
        string FirstName,
        string LastName,
        Roles RoleType
    ) : ICommand;

    public enum Roles
    {
        DOCTOR = 1,
        CLINIC_STAFF = 2
    }

    #endregion
}