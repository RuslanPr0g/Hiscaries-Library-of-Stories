using HC.Application.Write.Notifications.Command.BecomePublisher;

namespace HC.Application.Write.Notifications.Services;

public interface INotificationWriteService
{
    Task<OperationResult> ReadNotifications(ReadNotificationCommand command);
}