using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class GlobalOrder
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int OrderCode { get; set; }
}