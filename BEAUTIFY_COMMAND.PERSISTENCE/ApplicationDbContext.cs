using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BEAUTIFY_COMMAND.PERSISTENCE;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        builder.Entity<CustomerSchedule>()
            .HasOne(cs => cs.Customer)
            .WithMany(u => u.CustomerSchedules)
            .HasForeignKey(cs => cs.CustomerId)
            .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete
        builder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique()
            .HasAnnotation("SqlServer:CaseSensitive", true);
        builder.Entity<User>()
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();

        builder.Entity<SubscriptionPackage>()
            .HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Role>()
            .HasData(
                new Role { Id = Guid.NewGuid(), Name = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "Doctor" },
                new Role { Id = Guid.NewGuid(), Name = "Customer" },
                new Role { Id = Guid.NewGuid(), Name = "Staff" }
            );
        
    }
}