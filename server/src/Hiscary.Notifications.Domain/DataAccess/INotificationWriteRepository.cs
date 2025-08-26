using StackNucleus.DDD.Domain.Repositories;

namespace Hiscary.Notifications.Domain.DataAccess;

public interface INotificationWriteRepository : IBaseWriteRepository<Notification, NotificationId>
{
    Task<Notification?> GetByObjectReferenceId(Guid objectReferenceId);
}
