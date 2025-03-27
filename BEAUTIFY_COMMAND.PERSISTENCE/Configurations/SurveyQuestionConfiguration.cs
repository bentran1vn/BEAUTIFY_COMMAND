using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;

public class SurveyQuestionConfiguration: IEntityTypeConfiguration<SurveyQuestion>
{
    public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
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
                new SurveyQuestion
                {
                    Id = questionIds[0],
                    SurveyId = surveyDaId,
                    Question = "Sau khi rửa mặt (không bôi kem) da bạn thường cảm thấy thế nào?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[1],
                    SurveyId = surveyDaId,
                    Question = "Vào giữa ngày da bạn trông thế nào (nếu không thấm dầu)?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[2],
                    SurveyId = surveyDaId,
                    Question = "Tần suất bong tróc hoặc khô mảng?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[3],
                    SurveyId = surveyDaId,
                    Question = "Mức độ nhìn thấy lỗ chân lông?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[4],
                    SurveyId = surveyDaId,
                    Question = "Bạn có thường bị mụn hoặc tắc nghẽn lỗ chân lông?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[5],
                    SurveyId = surveyDaId,
                    Question = "Da bạn có khi nào vừa khô ở vài chỗ vừa dầu ở chỗ khác?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[6],
                    SurveyId = surveyDaId,
                    Question = "Phản ứng da khi dùng sản phẩm mới hoặc thời tiết thay đổi?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[7],
                    SurveyId = surveyDaId,
                    Question = "Nếu bỏ qua kem dưỡng một ngày da bạn thế nào?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[8],
                    SurveyId = surveyDaId,
                    Question = "Khi trang điểm lớp nền giữ trên da ra sao?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                },
                new SurveyQuestion
                {
                    Id = questionIds[9],
                    SurveyId = surveyDaId,
                    Question = "Tổng quát, câu mô tả nào hợp nhất với da bạn?",
                    QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                    CreatedOnUtc = DateTimeOffset.UtcNow
                }
            );
    }
}