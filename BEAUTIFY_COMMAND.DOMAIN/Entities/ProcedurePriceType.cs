using System.ComponentModel.DataAnnotations.Schema;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class ProcedurePriceType : AggregateRoot<Guid>, IAuditableEntity
{
    public string Name { get; set; } = default!;

    [Column(TypeName = "decimal(18,2)")] public decimal Price { get; set; }

    public int Duration { get; set; }


    public Guid ProcedureId { get; set; } = default!;
    public virtual Procedure Procedure { get; set; } = default!;

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}