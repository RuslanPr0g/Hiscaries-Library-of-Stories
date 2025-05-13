using Enterprise.Domain.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Enterprise.Persistence.Context;

public class EnterpriseDatabaseDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<EnterpriseContext>
{
    public EnterpriseContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        var connectionString = configuration.GetConnectionString("PostgresEF");

        var builder = new DbContextOptionsBuilder<EnterpriseContext>();
        builder.UseNpgsql(connectionString);
        return new EnterpriseContext(builder.Options);
    }
}

/// <summary>
/// Handles outbox pattern.
/// </summary>
public sealed class EnterpriseContext(DbContextOptions<EnterpriseContext> options) : DbContext(options)
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxConfiguration).Assembly);
    }
}
