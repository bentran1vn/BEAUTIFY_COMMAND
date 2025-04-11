using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;

public class CustomerScheduleReminderConfiguration : IEntityTypeConfiguration<CustomerScheduleReminder>
{
    public void Configure(EntityTypeBuilder<CustomerScheduleReminder> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.CustomerSchedule)
            .WithMany()
            .HasForeignKey(x => x.CustomerScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
