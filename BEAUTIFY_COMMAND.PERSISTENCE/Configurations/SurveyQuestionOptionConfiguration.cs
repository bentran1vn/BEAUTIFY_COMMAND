using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class SurveyQuestionOptionConfiguration : IEntityTypeConfiguration<SurveyQuestionOption>
{
    public void Configure(EntityTypeBuilder<SurveyQuestionOption> builder)
    {
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
            Guid.Parse("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa") // Q10
        };

        builder.HasData(
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                SurveyQuestionId = questionIds[0],
                Option =
                    "A) Rất căng khô hoặc bong tróc; B) Khá cân bằng không quá khô hay dầu; C) Hơi bóng ở vùng chữ T; D) Bóng dầu toàn mặt; E) Đỏ hoặc châm chích",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                SurveyQuestionId = questionIds[1],
                Option =
                    "A) Không hầu như chỉ khô; B) Không khá đồng đều; C) Thường khô ở má nhưng dầu vùng chữ T; D) Oily toàn mặt; E) Thay đổi theo độ nhạy cảm",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                SurveyQuestionId = questionIds[2],
                Option =
                    "A) Rất nhỏ hoặc gần như không thấy; B) Thấy ở mức vừa phải; C) Rõ hơn ở vùng chữ T; D) To và dễ thấy toàn mặt; E) Rõ hơn khi da ửng đỏ hoặc kích ứng",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                SurveyQuestionId = questionIds[3],
                Option =
                    "A) Rất căng và khó chịu; B) Khá bình thường; C) T-zone bóng má bình thường; D) Rất bóng hoặc nhờn; E) Đỏ hoặc ngứa",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                SurveyQuestionId = questionIds[4],
                Option =
                    "A) Khô hơn hoặc bong tróc; B) Thích nghi khá ổn; C) Có vùng dầu vùng không; D) Tăng tiết dầu nổi mụn; E) Kích ứng ửng đỏ",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                SurveyQuestionId = questionIds[5],
                Option =
                    "A) Thường xuyên; B) Hầu như không bao giờ; C) Thỉnh thoảng ở một số vùng; D) Rất hiếm; E) Do nhạy cảm với sản phẩm hoặc thời tiết",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                SurveyQuestionId = questionIds[6],
                Option =
                    "A) Dễ bám vào vùng khô; B) Khá đều cần ít dặm lại; C) Xuống tông hoặc bóng ở chữ T; D) Trôi hoặc bóng dầu gần như toàn mặt; E) Kích ứng hoặc ửng đỏ",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                SurveyQuestionId = questionIds[7],
                Option =
                    "A) Rất hiếm; B) Đôi khi; C) Chủ yếu ở vùng chữ T; D) Thường xuyên hoặc toàn mặt; E) Phụ thuộc độ nhạy cảm với sản phẩm",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                SurveyQuestionId = questionIds[8],
                Option =
                    "A) Rất khô và hay căng; B) Cân bằng không quá khô dầu; C) Vừa dầu vừa khô da hỗn hợp; D) Dầu toàn mặt; E) Rất nhạy cảm hoặc dễ kích ứng",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.Parse("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                SurveyQuestionId = questionIds[9],
                Option =
                    "A) Vẫn khô hoặc căng; B) Khá cân bằng ít bóng; C) Có chút bóng ở vùng chữ T; D) Bóng dầu toàn khuôn mặt; E) Dễ kích ứng hoặc ửng đỏ",
                CreatedOnUtc = DateTimeOffset.UtcNow
            }
        );
    }
}