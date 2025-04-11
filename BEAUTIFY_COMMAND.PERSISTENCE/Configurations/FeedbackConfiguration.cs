using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            // Configure the relationship with OrderDetail
            builder
                .HasOne<OrderDetail>()
                .WithOne(od => od.Feedback)
                .HasForeignKey<Feedback>(f => f.OrderDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
