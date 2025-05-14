using Enterprise.Domain;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Persistence.Context;

/// <summary>
/// Base context class.
/// </summary>
public abstract class BaseEnterpriseContext<TContext>(DbContextOptions<TContext> options) 
    : DbContext(options)
    where TContext : DbContext
{
    public abstract string SchemaName { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaName);
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
