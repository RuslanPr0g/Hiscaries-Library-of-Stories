using HC.Application.Read.Notifications.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HC.Infrastructure.SignalR.Hubs;

[Authorize]
public sealed class UserNotificationHub : Hub
{
    private readonly INotificationReadRepository _notificationRepository;

    public UserNotificationHub(INotificationReadRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

        if (Guid.TryParse(userId, out var id))
        {
            var missedNotifications = await _notificationRepository.GetMissedNotificationsByUserId(id);

            foreach (var notification in missedNotifications)
            {
                await Clients.Caller.SendAsync(notification.Type, notification.Message);
            }

            await base.OnConnectedAsync();
        }
    }
}
