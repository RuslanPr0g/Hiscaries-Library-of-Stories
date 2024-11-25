namespace HC.Application.Common.UserNotifications;

public interface IUserNotificationHub
{
    Task SendNotification(string message);
}
