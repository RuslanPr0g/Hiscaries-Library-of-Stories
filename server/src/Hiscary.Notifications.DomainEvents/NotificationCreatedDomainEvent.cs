using StackNucleus.DDD.Domain.Events;

namespace Hiscary.Notifications.DomainEvents;

public sealed class NotificationCreatedDomainEvent(
    Guid Id,
    Guid UserId,
    string Type,
    string Message) : BaseDomainEvent
{
    public Guid Id { get; set; } = Id;
    public string Type { get; set; } = Type;
    public string Message { get; set; } = Message;
    public Guid UserId { get; set; } = UserId;
}
