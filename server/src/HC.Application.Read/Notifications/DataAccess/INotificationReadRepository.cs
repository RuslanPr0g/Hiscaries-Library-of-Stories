using HC.Application.Read.Notifications.ReadModels;
using HC.Domain.UserAccounts;

namespace HC.Application.Read.Notifications.DataAccess;

public interface INotificationReadRepository
{
    Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(UserAccountId userId);
    Task<IEnumerable<NotificationReadModel>> GetNotificationsByUserId(UserAccountId userId);
}
