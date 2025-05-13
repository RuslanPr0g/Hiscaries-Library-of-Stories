using HC.Application.Read.Notifications.ReadModels;

namespace HC.Application.Read.Notifications.DataAccess;

public interface INotificationReadRepository
{
    Task<NotificationReadModel?> GetById(NotificationId id);
    Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(UserAccountId userId);
    Task<IEnumerable<NotificationReadModel>> GetNotificationsByUserId(UserAccountId userId);
}
