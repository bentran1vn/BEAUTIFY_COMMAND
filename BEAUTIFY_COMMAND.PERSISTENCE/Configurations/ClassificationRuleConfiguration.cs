using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;

public class ClassificationRuleConfiguration: IEntityTypeConfiguration<ClassificationRule>
{
    public void Configure(EntityTypeBuilder<ClassificationRule> builder)
    {
        var surveyDaId = Guid.Parse("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345");
        
        Guid[] questionIds =
        {
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-111111111111"), // Q1
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-222222222222"), // Q2
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-333333333333"), // Q3
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-444444444444"), // Q4
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-555555555555"), // Q5
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-666666666666"), // Q6
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-777777777777"), // Q7
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-888888888888"), // Q8
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-999999999999"), // Q9
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa")  // Q10
        };
        
         builder.HasData(
            // Q1, Option A => Da khô
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-1111-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "A",
                ClassificationLabel = "Da khô",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option B => Da thường
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-2222-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "B",
                ClassificationLabel = "Da thường",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option C => Da hỗn hợp
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-3333-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "C",
                ClassificationLabel = "Da hỗn hợp",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option D => Da dầu
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-4444-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "D",
                ClassificationLabel = "Da dầu",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option E => Da nhạy cảm
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-5555-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "E",
                ClassificationLabel = "Da nhạy cảm",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            }
        );
    }
}