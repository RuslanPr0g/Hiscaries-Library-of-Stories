namespace HC.Notifications.Domain.Notifications;

public sealed record NotificationId(Guid Value) : Identity(Value)
{
    public static implicit operator NotificationId(Guid identity) => new(identity);
    public static implicit operator Guid(NotificationId identity) => identity.Value;
}
