using HC.Application.Write.Notifications.Command.BecomePublisher;
using HC.Application.Write.ResultModels.Response;

namespace HC.Application.Write.Notifications.Services;

public interface INotificationWriteService
{
    Task<OperationResult> ReadNotifications(ReadNotificationCommand command);
}