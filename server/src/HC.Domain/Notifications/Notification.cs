using HC.Domain.Notifications.Events;
using HC.Domain.UserAccounts;

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
        string type) : base(id)
    {
        UserId = userId;
        Message = message;
        Type = type;

        IsRead = false;

        PublishNotificationCreatedEvent();
    }

    public static Notification CreateStoryPublishedNotification(
        NotificationId id,
        UserAccountId userId,
        string storyTitle)
    {
        return new Notification(id, userId, $"Story was published: {storyTitle}", "StoryPublished");
    }

    public UserAccountId UserId { get; }
    public string Message { get; }
    public bool IsRead { get; private set; }
    public string Type { get; }

    public void Read()
    {
        IsRead = true;
    }

    private void PublishNotificationCreatedEvent()
    {
        PublishEvent(new NotificationCreatedDomainEvent(UserId, Type, Message));
    }

    private Notification()
    {
    }
}