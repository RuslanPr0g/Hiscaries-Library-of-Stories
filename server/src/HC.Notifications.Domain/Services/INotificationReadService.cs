using HC.Notifications.Domain.ReadModels;

namespace HC.Notifications.Application.Read.Notifications.Services;

public interface INotificationReadService
{
    Task<IEnumerable<NotificationReadModel>> GetNotifications(UserAccountId userId);
}