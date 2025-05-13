using HC.Notifications.Application.Read.Notifications.ReadModels;

namespace HC.Notifications.Application.Read.Notifications.Services;

public interface INotificationReadService
{
    Task<IEnumerable<NotificationReadModel>> GetNotifications(UserAccountId userId);
}