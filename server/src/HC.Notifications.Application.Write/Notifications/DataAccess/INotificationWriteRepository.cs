using HC.Domain.Notifications;

namespace HC.Application.Write.Notifications.DataAccess;

public interface INotificationWriteRepository
{
    Task<Notification?> GetById(NotificationId id);
    Task<IEnumerable<Notification>> GetByIds(NotificationId[] ids);
    Task Add(Notification notification);
}
