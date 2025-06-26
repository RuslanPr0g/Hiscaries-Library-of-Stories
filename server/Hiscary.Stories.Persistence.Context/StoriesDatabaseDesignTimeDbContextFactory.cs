using StackNucleus.DDD.Persistence.EF.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Hiscary.Stories.Persistence.Context;

public class StoriesDatabaseDesignTimeDbContextFactory
    : NucleusDatabaseDesignTimeDbContextFactory<StoriesContext>
{
    public override StoriesContext CreateDbContextBasedOnOptions(
        DbContextOptions<StoriesContext> options)
    {
        return new StoriesContext(options);
    }
}
