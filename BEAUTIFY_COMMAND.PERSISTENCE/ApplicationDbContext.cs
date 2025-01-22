using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.PERSISTENCE;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<User> Users { get; set; }
}