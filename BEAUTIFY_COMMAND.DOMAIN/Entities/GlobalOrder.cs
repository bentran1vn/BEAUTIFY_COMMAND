using System.ComponentModel.DataAnnotations.Schema;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class GlobalOrder : Entity<Guid>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderCode { get; init; }
}