using Enterprise.Persistence.Context;
using HC.PlatformUsers.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HC.PlatformUsers.Persistence.Context;

public sealed class PlatformUsersContext(DbContextOptions<PlatformUsersContext> options)
    : BaseEnterpriseContext<PlatformUsersContext>(options)
{
    public override string SchemaName => "platformusers";

    public DbSet<PlatformUser> PlatformUsers { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<PlatformUserToLibrarySubscription> PlatformUserToLibrarySubscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
