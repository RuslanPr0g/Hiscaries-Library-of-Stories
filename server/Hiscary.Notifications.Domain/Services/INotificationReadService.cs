using Hiscary.Notifications.Domain.ReadModels;

namespace Hiscary.Notifications.Domain.Services;

public interface INotificationReadService
{
    Task<IEnumerable<NotificationReadModel>> GetNotifications(Guid userAccountId);
}