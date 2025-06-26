using Hiscary.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Hiscary.Stories.Persistence.Context;

public class StoriesDatabaseDesignTimeDbContextFactory
    : EnterpriseDatabaseDesignTimeDbContextFactory<StoriesContext>
{
    public override StoriesContext CreateDbContextBasedOnOptions(
        DbContextOptions<StoriesContext> options)
    {
        return new StoriesContext(options);
    }
}
