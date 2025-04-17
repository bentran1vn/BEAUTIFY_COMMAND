using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasData(
                new Role { Id = new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), Name = "System Admin" },
                new Role { Id = new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), Name = "System Staff" },
                new Role { Id = new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), Name = "Doctor" },
                new Role { Id = new Guid("b5db3ea1-f81c-465e-a23b-da7d6d361930"), Name = "Customer" },
                new Role { Id = new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), Name = "Clinic Admin" },
                new Role { Id = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), Name = "Clinic Staff" }
            );
    }
}