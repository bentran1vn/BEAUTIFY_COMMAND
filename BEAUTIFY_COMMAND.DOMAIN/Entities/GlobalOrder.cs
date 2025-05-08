namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class GlobalOrder : AggregateRoot<int>, IAuditableEntity
{
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}