using StackNucleus.DDD.Persistence.EF.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Hiscary.Notifications.Persistence.Context;

public class NotificationsDatabaseDesignTimeDbContextFactory
    : EnterpriseDatabaseDesignTimeDbContextFactory<NotificationsContext>
{
    public override NotificationsContext CreateDbContextBasedOnOptions(
        DbContextOptions<NotificationsContext> options)
    {
        return new NotificationsContext(options);
    }
}
