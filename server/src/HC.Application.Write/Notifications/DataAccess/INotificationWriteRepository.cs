using HC.Domain.Notifications;

namespace HC.Application.Write.Notifications.DataAccess;

public interface INotificationWriteRepository
{
    Task<Notification?> GetById(NotificationId id);
    Task Add(Notification notification);
}
