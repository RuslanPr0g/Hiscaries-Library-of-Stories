using Enterprise.Domain.ResultModels.Response;

namespace HC.Notifications.Domain.Services;

public interface INotificationWriteService
{
    Task<OperationResult> ReadNotifications(Guid userId, Guid[] notificationIds);
}