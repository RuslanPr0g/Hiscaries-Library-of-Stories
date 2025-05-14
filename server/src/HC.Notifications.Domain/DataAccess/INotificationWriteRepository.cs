using Enterprise.Domain.DataAccess;
using HC.Notifications.Domain.Notifications;

namespace HC.Notifications.Domain.DataAccess;

public interface INotificationWriteRepository : IBaseWriteRepository<Notification, NotificationId>
{
}
