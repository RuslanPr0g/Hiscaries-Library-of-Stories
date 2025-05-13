using HC.Notifications.Application.Read.Notifications.ReadModels;

namespace HC.Notifications.Application.Read.Notifications.Queries.GetUserNotifications;

public sealed class GetUserNotificationsQuery : IRequest<IEnumerable<NotificationReadModel>>
{
    public Guid UserId { get; set; }
}