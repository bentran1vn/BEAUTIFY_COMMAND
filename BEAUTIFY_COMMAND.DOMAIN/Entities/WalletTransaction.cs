using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class WalletTransaction : AggregateRoot<Guid>, IAuditableEntity
{

        
    public Guid? UserId { get; set; }
    public virtual User? User { get; set; }

    public Guid? ClinicId { get; set; }
    public virtual Clinic? Clinic { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }


    // Transaction Type: 0 = Deposit, 1 = Withdrawal, 2 = Transfer, 3 = SERVICE_DEPOSIT, 4 = SERVICE_DEPOSIT_REFUND
    [MaxLength(30)] public required string TransactionType { get; set; }

    // Status: 0 = Pending, 1 = Completed, 2 = Failed, 3 = Cancelled
    [MaxLength(20)] public required string Status { get; set; }

    public bool IsMakeBySystem { get; set; } = true;

    [MaxLength(255)] public string? Description { get; set; }

    public DateTime TransactionDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

    // For service booking deposits and refunds
    public Guid? OrderId { get; set; }
    public virtual Order? Order { get; set; }

    public string? NewestQrUrl { get; set; } = null;

    // For refund transactions, reference to the original deposit transaction
    public Guid? RelatedTransactionId { get; set; }

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}