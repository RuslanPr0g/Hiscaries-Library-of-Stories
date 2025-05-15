using HC.Notifications.Domain.ReadModels;

namespace HC.Notifications.Domain.Services;

public interface INotificationReadService
{
    Task<IEnumerable<NotificationReadModel>> GetNotifications(Guid userAccountId);
}