using HC.Notifications.Application.Read.Notifications.ReadModels;

namespace HC.Notifications.Application.Read.Notifications.DataAccess;

public interface INotificationReadRepository
{
    Task<NotificationReadModel?> GetById(NotificationId id);
    Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(UserAccountId userId);
    Task<IEnumerable<NotificationReadModel>> GetNotificationsByUserId(UserAccountId userId);
}
