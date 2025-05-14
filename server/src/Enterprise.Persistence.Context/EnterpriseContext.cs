using Enterprise.Domain.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Persistence.Context;


/// <summary>
/// Handles outbox pattern.
/// </summary>
public sealed class EnterpriseContext(DbContextOptions<EnterpriseContext> options) 
    : BaseEnterpriseContext<EnterpriseContext>(options)
{
    public override string SchemaName => "enterprise";

    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxConfiguration).Assembly);
    }
}
