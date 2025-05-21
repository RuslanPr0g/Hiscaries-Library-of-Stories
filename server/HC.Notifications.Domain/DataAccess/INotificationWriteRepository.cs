using Enterprise.Domain.DataAccess;

namespace HC.Notifications.Domain.DataAccess;

public interface INotificationWriteRepository : IBaseWriteRepository<Notification, NotificationId>
{
    Task<Notification?> GetByObjectReferenceId(Guid objectReferenceId);
}
