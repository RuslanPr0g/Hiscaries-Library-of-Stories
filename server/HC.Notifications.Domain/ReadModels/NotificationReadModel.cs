using Enterprise.Domain.ReadModels;

namespace HC.Notifications.Domain.ReadModels;

public class NotificationReadModel : IReadModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public bool IsRead { get; set; }
    public string Type { get; set; }
    public Guid? RelatedObjectId { get; set; }
    public string? PreviewUrl { get; set; }

    public static NotificationReadModel FromDomainModel(Notification notification)
    {
        return new()
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Message = notification.Message,
            IsRead = notification.IsRead,
            Type = notification.Type,
            RelatedObjectId = notification.RelatedObjectId,
            PreviewUrl = notification.PreviewUrl
        };
    }
}
