using HC.Application.Read.Notifications.ReadModels;
using HC.Domain.UserAccounts;

namespace HC.Application.Read.Notifications.Services;

public interface INotificationReadService
{
    Task<IEnumerable<NotificationReadModel>> GetNotifications(UserAccountId userId);
}