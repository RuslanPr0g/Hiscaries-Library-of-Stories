using HC.Application.Read.Users.ReadModels;
using HC.Domain.UserAccounts;

namespace HC.Application.Read.Notifications.DataAccess;

public interface INotificationReadRepository
{
    Task<IEnumerable<NotificationReadModel>> GetMissedNotificationsByUserId(UserAccountId userId);
}
