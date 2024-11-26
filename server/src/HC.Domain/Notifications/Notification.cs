using HC.Domain.Notifications.Events;

namespace HC.Domain.Notifications;

/// <summary>
/// Represents a notification in the system
/// </summary>
public sealed class Notification : AggregateRoot<NotificationId>
{
    public Notification(
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

    public UserAccountId UserId { get; }
    public string Message { get; }
    public bool IsRead { get; }
    public string Type { get; }

    private void PublishNotificationCreatedEvent()
    {
        PublishEvent(new NotificationCreatedDomainEvent(UserId, Type, Message));
    }

    private Notification()
    {
    }
}