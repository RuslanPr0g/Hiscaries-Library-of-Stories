using Hiscary.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Hiscary.PlatformUsers.Persistence.Context;

public class PlatformUsersDatabaseDesignTimeDbContextFactory
    : EnterpriseDatabaseDesignTimeDbContextFactory<PlatformUsersContext>
{
    public override PlatformUsersContext CreateDbContextBasedOnOptions(
        DbContextOptions<PlatformUsersContext> options)
    {
        return new PlatformUsersContext(options);
    }
}
