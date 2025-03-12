using System.ComponentModel.DataAnnotations;

namespace BEAUTIFY_COMMAND.DOMAIN.Entities;
public class ClassificationRule : AggregateRoot<Guid>, IAuditableEntity
{
    public Guid SurveyId { get; set; }
    public virtual Survey? Survey { get; set; }
    public Guid SurveyQuestionId { get; set; }
    public virtual SurveyQuestion? SurveyQuestion { get; set; }
    [MaxLength(100)] public string? OptionValue { get; set; }
    [MaxLength(100)] public string? ClassificationLabel { get; set; }
    public int Points { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }
}