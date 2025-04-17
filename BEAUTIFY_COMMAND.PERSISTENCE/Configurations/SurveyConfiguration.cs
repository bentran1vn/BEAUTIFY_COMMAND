using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.HasData(
            new Survey
            {
                Id = Guid.Parse("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                Name = "Khảo sát da",
                Description = "Nhận biết loại da",
                CategoryId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                CreatedOnUtc = DateTimeOffset.UtcNow
            }
        );
    }
}