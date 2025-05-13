using HC.Notifications.Application.Write.Notifications.Command.BecomePublisher;

namespace HC.Notifications.Application.Write.Notifications.Services;

public interface INotificationWriteService
{
    Task<OperationResult> ReadNotifications(ReadNotificationCommand command);
}