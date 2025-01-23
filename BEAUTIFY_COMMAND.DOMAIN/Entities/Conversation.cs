namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class Conversation : AggregateRoot<Guid>, IAuditableEntity
{

    public string Type { get; set; }


    public virtual ICollection<Message>? Messages { get; set; }
    public virtual ICollection<UserConversation>? UserConversations { get; set; }

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}