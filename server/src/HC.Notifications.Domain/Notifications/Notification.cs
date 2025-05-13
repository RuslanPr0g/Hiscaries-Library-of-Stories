using HC.Domain.Notifications.Events;

namespace HC.Domain.Notifications;

/// <summary>
/// Represents a notification in the system
/// </summary>
public sealed class Notification : AggregateRoot<NotificationId>
{
    private Notification(
        NotificationId id,
        UserAccountId userId,
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

    public static Notification CreateStoryPublishedNotification(
        NotificationId id,
        UserAccountId userId,
        string storyTitle,
        StoryId storyId,
        string? previewUrl)
    {
        return new Notification(
            id,
            userId,
            $"{storyTitle}",
            "StoryPublished",
            storyId,
            previewUrl);
    }

    public UserAccountId UserId { get; }
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