using Enterprise.Domain;

namespace HC.Notifications.Domain.Events;

public sealed class NotificationCreatedDomainEvent(
    Guid Id,
    Guid UserId,
    string Type,
    string Message) : IDomainEvent
{
    public Guid Id { get; set; } = Id;
    public string Type { get; set; } = Type;
    public string Message { get; set; } = Message;
    public Guid UserId { get; set; } = UserId;
}
