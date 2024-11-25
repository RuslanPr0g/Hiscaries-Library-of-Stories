using HC.Application.Common.UserNotifications;
using Microsoft.AspNetCore.SignalR;

namespace HC.Infrastructure.SignalR.Hubs;

public sealed class UserNotificationHub : Hub, IUserNotificationHub
{
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
