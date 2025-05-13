using HC.Application.Read.Notifications.ReadModels;

namespace HC.Application.Read.Notifications.Services;

public interface INotificationReadService
{
    Task<IEnumerable<NotificationReadModel>> GetNotifications(UserAccountId userId);
}