using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Notifications.Command.BecomePublisher;

public sealed class ReadNotificationCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid[] NotificationIds { get; set; }
}