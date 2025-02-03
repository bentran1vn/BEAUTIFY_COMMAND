using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

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

        
    }
    // public DbSet<User> Users { get; set; }

}