using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionPackage>
{
    public void Configure(EntityTypeBuilder<SubscriptionPackage> builder)
    {
        builder
            .HasData(
                new SubscriptionPackage
                {
                    Id = new Guid("4b7171f4-3219-4688-9f7c-625687a95867"),
                    Name = "Dùng Thử",
                    Description = "Dùng Thử",
                    Price = 0,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 0,
                    LimitLiveStream = 1,
                    EnhancedViewer = 0
                },
                new SubscriptionPackage
                {
                    Id = new Guid("248bf96b-9782-4011-8bb0-b26e66658090"),
                    Name = "Đồng",
                    Description = "Đồng",
                    Price = 3000,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 1,
                    LimitLiveStream = 5,
                    EnhancedViewer = 0
                },
                new SubscriptionPackage
                {
                    Id = new Guid("b549752a-f156-4894-90ad-ab3994fd071d"),
                    Name = "Bạc",
                    Description = "Bạc",
                    Price = 5200,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 5,
                    LimitLiveStream = 10,
                    EnhancedViewer = 100
                },
                new SubscriptionPackage
                {
                    Id = new Guid("b5db3ea1-f81c-465e-a23b-da7d6d361930"),
                    Name = "Vàng",
                    Description = "Vàng",
                    Price = 9000000,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 10,
                    LimitLiveStream = 20,
                    EnhancedViewer = 200
                }
            );
    }
}