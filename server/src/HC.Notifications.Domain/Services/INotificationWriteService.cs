using Enterprise.Domain.ResultModels;

namespace HC.Notifications.Domain.Services;

public interface INotificationWriteService
{
    Task<OperationResult> ReadNotifications(Guid userId, Guid[] notificationIds);
}