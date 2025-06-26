using Enterprise.Persistence.Context;
using Hiscary.Stories.Domain.Genres;
using Hiscary.Stories.Domain.Stories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Hiscary.Stories.Persistence.Context;

public sealed class StoriesContext(DbContextOptions<StoriesContext> options)
    : BaseEnterpriseContext<StoriesContext>(options)
{

    public static string SCHEMA_NAME = "stories";

    public override string SchemaName => SCHEMA_NAME;

    public DbSet<Story> Stories { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
