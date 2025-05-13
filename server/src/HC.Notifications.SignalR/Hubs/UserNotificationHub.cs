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
