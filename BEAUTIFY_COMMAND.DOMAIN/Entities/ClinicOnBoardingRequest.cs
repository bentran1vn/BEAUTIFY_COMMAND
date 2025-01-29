using System.ComponentModel.DataAnnotations;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class ClinicOnBoardingRequest : AggregateRoot<Guid>, IAuditableEntity
{
    [MaxLength(100)] public required string Name { get; set; }
    [MaxLength(100)] public required string Email { get; set; }
    [MaxLength(15)] public required string PhoneNumber { get; set; }
    [MaxLength(500)] public required string Address { get; set; }
    [MaxLength(20)] public required string TaxCode { get; set; }
    [MaxLength(250)] public required string BusinessLicenseUrl { get; set; }
    [MaxLength(250)] public required string OperatingLicenseUrl { get; set; }
    public DateTimeOffset? OperatingLicenseExpiryDate { get; set; }
    [MaxLength(50)] public string? Status { get; set; }
    [MaxLength(250)] public string? RejectReason { get; set; }
    

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}