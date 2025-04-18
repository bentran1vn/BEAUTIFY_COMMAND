using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class LiveStreamDetailConfiguration : IEntityTypeConfiguration<LiveStreamDetail>
{
    public void Configure(EntityTypeBuilder<LiveStreamDetail> builder)
    {
        // Configure the relationship with LivestreamRoom
    }
}