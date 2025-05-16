using HC.Notifications.Domain;
using HC.Notifications.Domain.DataAccess;
using HC.Notifications.Persistence.Context;

namespace HC.Notifications.Persistence.Write;

public class NotificationWriteRepository(NotificationsContext context)
    : BaseWriteRepository<Notification, NotificationId, NotificationsContext>,
    INotificationWriteRepository
{
    protected override NotificationsContext Context { get; init; } = context;
}
