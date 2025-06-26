using Enterprise.Domain.DataAccess;

namespace Hiscary.Notifications.Domain.DataAccess;

public interface INotificationWriteRepository : IBaseWriteRepository<Notification, NotificationId>
{
    Task<Notification?> GetByObjectReferenceId(Guid objectReferenceId);
}
