using Enterprise.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Notifications.Persistence.Context;

public class NotificationsDatabaseDesignTimeDbContextFactory
    : EnterpriseDatabaseDesignTimeDbContextFactory<NotificationsContext>
{
    public override NotificationsContext CreateDbContextBasedOnOptions(
        DbContextOptions<NotificationsContext> options)
    {
        return new NotificationsContext(options);
    }
}
