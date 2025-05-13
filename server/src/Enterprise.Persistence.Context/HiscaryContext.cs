using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Enterprise.Persistence.Context;

// TODO: CREATE ENTERPRISE CONTEXT AND THEN NEST IT EVERYWHERE

public class DatabaseDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<HiscaryContext>
{
    public HiscaryContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        var connectionString = configuration.GetConnectionString("PostgresEF");

        var builder = new DbContextOptionsBuilder<HiscaryContext>();
        builder.UseNpgsql(connectionString);
        return new HiscaryContext(builder.Options);
    }
}

public sealed class HiscaryContext(DbContextOptions<HiscaryContext> options) : DbContext(options)
{
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<PlatformUser> PlatformUsers { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public DbSet<PlatformUserToLibrarySubscription> PlatformUserToLibrarySubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        SetAuditFields();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditableEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (IAuditableEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.EditedAt = DateTime.UtcNow;
                entity.Version = 1;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.EditedAt = DateTime.UtcNow;
                entity.Version++;
            }
        }
    }
}