using StackNucleus.DDD.Persistence.EF.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Hiscary.PlatformUsers.Persistence.Context;

public class PlatformUsersDatabaseDesignTimeDbContextFactory
    : NucleusDatabaseDesignTimeDbContextFactory<PlatformUsersContext>
{
    public override PlatformUsersContext CreateDbContextBasedOnOptions(
        DbContextOptions<PlatformUsersContext> options)
    {
        return new PlatformUsersContext(options);
    }
}
