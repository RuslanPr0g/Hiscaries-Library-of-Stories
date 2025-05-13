using HC.Notifications.Application.Read.Notifications.ReadModels;
using HC.Notifications.Application.Read.Notifications.Services;

namespace HC.Notifications.Application.Read.Notifications.Queries.GetUserNotifications;

public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, IEnumerable<NotificationReadModel>>
{
    private readonly INotificationReadService _service;

    public GetUserNotificationsQueryHandler(INotificationReadService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<NotificationReadModel>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetNotifications(request.UserId);
    }
}