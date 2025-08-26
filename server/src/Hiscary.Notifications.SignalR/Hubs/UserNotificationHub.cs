using Hiscary.Notifications.Domain.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Hiscary.Notifications.SignalR.Hubs;

[Authorize]
public sealed class UserNotificationHub(INotificationReadRepository notificationRepository) : Hub
{
    private readonly INotificationReadRepository _notificationRepository = notificationRepository;

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;

        if (Guid.TryParse(userId, out var id))
        {
            var missedNotifications = await _notificationRepository.GetMissedNotificationsByUserId(id);

            foreach (var notification in missedNotifications)
            {
                await Clients.Caller.SendAsync(notification.Type, notification);
            }

            await base.OnConnectedAsync();
        }
    }
}
