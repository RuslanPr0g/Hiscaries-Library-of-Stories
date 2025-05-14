using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Enterprise.Persistence.Context;

public abstract class EnterpriseDatabaseDesignTimeDbContextFactory<TContext>
    : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    public virtual string ConnectionStringName { get; set; } = "PostgresEF";
    public virtual string AppSettingsName { get; set; } = "appsettings.json";

    public abstract TContext CreateDbContextBasedOnOptions(DbContextOptions<TContext> options);

    public TContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsName)
                .Build();

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        var builder = new DbContextOptionsBuilder<TContext>();
        builder.UseNpgsql(connectionString);
        return CreateDbContextBasedOnOptions(builder.Options);
    }
}
