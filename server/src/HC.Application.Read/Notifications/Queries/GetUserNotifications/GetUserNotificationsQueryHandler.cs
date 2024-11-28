using HC.Application.Read.Notifications.ReadModels;
using HC.Application.Read.Notifications.Services;
using MediatR;

namespace HC.Application.Read.Notifications.Queries;

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