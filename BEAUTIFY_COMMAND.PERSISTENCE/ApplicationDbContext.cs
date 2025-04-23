using System.Reflection;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.PERSISTENCE;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<TriggerOutbox> TriggerOutboxs { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }
    public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public virtual DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public virtual DbSet<SurveyResponse> SurveyResponses { get; set; }
    public virtual DbSet<ClassificationRule> ClassificationRules { get; set; }
    public virtual DbSet<CustomerScheduleReminder> CustomerScheduleReminders { get; set; }
    public virtual DbSet<ClinicTransaction> ClinicTransactions { get; set; }
    public virtual DbSet<Procedure> Procedures { get; set; }
    public virtual DbSet<ProcedurePriceType> ProcedurePriceTypes { get; set; }

    private static void SetSoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : Entity<T>
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // In your ApplicationDbContext.OnModelCreating method
        // builder.Entity<Order>()
        //     .HasOne(o => o.OrderFeedback)
        //     .WithMany() // or .WithMany(of => of.Orders) if there's a collection
        //     .HasForeignKey(o => o.OrderFeedbackId)
        //     .IsRequired(false);

        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        builder.Entity<CustomerSchedule>()
            .HasOne(cs => cs.Customer)
            .WithMany(u => u.CustomerSchedules)
            .HasForeignKey(cs => cs.CustomerId)
            .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete

        builder.Entity<Clinic>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Category>()
            .HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<SubscriptionPackage>()
            .HasQueryFilter(x => !x.IsDeleted);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.BaseType is not { IsGenericType: true } ||
                entityType.ClrType.BaseType.GetGenericTypeDefinition() != typeof(Entity<>)) continue;
            var method = typeof(ApplicationDbContext)
                .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityType.ClrType);

            method?.Invoke(null, [builder]);
        }

        builder.Entity<Service>().Property(x => x.Description).HasColumnType("text");
        builder.Entity<Procedure>().Property(x => x.Description).HasColumnType("text");

        builder.Entity<Staff>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<DoctorCertificate>()
            .HasQueryFilter(x => !x.IsDeleted); // Add matching filter for DoctorCertificate
        builder.Entity<UserClinic>().HasQueryFilter(x => !x.IsDeleted); // Add matching filter for UserClinic
        builder.Entity<CustomerSchedule>()
            .HasQueryFilter(x => !x.IsDeleted); // Add matching filter for CustomerSchedule
        builder.Entity<ClinicOnBoardingRequest>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ClinicVoucher>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<ClinicService>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<SystemTransaction>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Promotion>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<DoctorService>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<LiveStreamDetail>().HasQueryFilter(x => !x.IsDeleted); // Add filter for LiveStreamDetail
        builder.Entity<Feedback>().HasQueryFilter(x => !x.IsDeleted); // Add filter for Feedback
        builder.Entity<CustomerScheduleReminder>()
            .HasQueryFilter(x => !x.IsDeleted); // Add filter for CustomerScheduleReminder

        builder.Entity<WorkingSchedule>().HasQueryFilter(x => !x.IsDeleted);

        builder.Entity<LivestreamRoom>()
            .HasOne(lr => lr.LiveStreamDetail)
            .WithOne() // Assuming one-to-one relationship, adjust if it's one-to-many
            .HasForeignKey<LivestreamRoom>(lr => lr.LiveStreamDetailId)
            .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed
        
        builder.Entity<Order>()
            .HasOne(lr => lr.OrderFeedback)
            .WithOne() // Assuming one-to-one relationship, adjust if it's one-to-many
            .HasForeignKey<Order>(lr => lr.OrderFeedbackId)
            .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed
        
        builder.Entity<CustomerSchedule>()
            .HasOne(lr => lr.Feedback)
            .WithOne() // Assuming one-to-one relationship, adjust if it's one-to-many
            .HasForeignKey<CustomerSchedule>(lr => lr.FeedbackId)
            .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed
        
        // builder.Entity<Procedure>()
        //     .HasMany(lr => lr.ProcedurePriceTypes)
        //     .WithOne(o => o.Procedure) // Assuming one-to-one relationship, adjust if it's one-to-many
        //     .HasForeignKey(lr => lr.ProcedureId)
        //     .OnDelete(DeleteBehavior.Restrict); // Adjust delete behavior as needed
        
        builder.Entity<ProcedurePriceType>()
            .HasOne(p => p.Procedure)
            .WithMany(p => p.ProcedurePriceTypes)
            .HasForeignKey(p => p.ProcedureId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}