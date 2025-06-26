using StackNucleus.DDD.Domain.DataAccess;
using Hiscary.Notifications.Domain.ReadModels;

namespace Hiscary.Notifications.Domain.DataAccess;

public interface INotificationReadRepository : IBaseReadRepository<NotificationReadModel>
{
    Task<NotificationReadModel?> GetById(Guid id);
    Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(Guid userAccountId);
    Task<IEnumerable<NotificationReadModel>> GetNotificationsByUserId(Guid userAccountId);
}
