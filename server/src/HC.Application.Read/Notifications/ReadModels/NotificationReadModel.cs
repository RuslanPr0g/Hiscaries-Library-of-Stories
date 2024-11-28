using HC.Domain.Notifications;

namespace HC.Application.Read.Notifications.ReadModels;

public class NotificationReadModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public string Type { get; set; }

    public static NotificationReadModel FromDomainModel(Notification notification)
    {
        return new()
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Message = notification.Message,
            IsRead = notification.IsRead,
            Type = notification.Type,
        };
    }
}
