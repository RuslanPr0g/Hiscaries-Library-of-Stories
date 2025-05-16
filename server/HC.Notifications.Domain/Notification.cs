using Enterprise.Domain;
using HC.Notifications.Domain.Events;

namespace HC.Notifications.Domain;

public sealed class Notification : AggregateRoot<NotificationId>
{
    private Notification(
        NotificationId id,
        Guid userId,
        string message,
        string type,
        Guid? refId = null,
        string? previewUrl = null) : base(id)
    {
        UserId = userId;
        Message = message;
        Type = type;
        RelatedObjectId = refId;
        PreviewUrl = previewUrl;

        IsRead = false;

        PublishNotificationCreatedEvent();
    }

    public static Notification CreatePublishedNotification(
        NotificationId id,
        Guid userId,
        string message,
        string type,
        Guid objectReferenceId,
        string? previewUrl)
    {
        return new Notification(
            id,
            userId,
            message,
            type,
            objectReferenceId,
            previewUrl);
    }

    public Guid UserId { get; }
    public string Message { get; }
    public bool IsRead { get; private set; }
    public string Type { get; }
    public Guid? RelatedObjectId { get; }
    public string? PreviewUrl { get; }

    public void Read()
    {
        IsRead = true;
    }

    private void PublishNotificationCreatedEvent()
    {
        PublishEvent(new NotificationCreatedDomainEvent(Id, UserId, Type, Message));
    }

    private Notification()
    {
    }
}