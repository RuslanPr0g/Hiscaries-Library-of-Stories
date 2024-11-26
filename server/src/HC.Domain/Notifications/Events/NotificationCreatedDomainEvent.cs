using System;

namespace HC.Domain.Notifications.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class NotificationCreatedDomainEvent(Guid UserId, string Type, string Message) : IDomainEvent
{
    public string Type { get; set; } = Type;
    public string Message { get; set; } = Message;
    public Guid UserId { get; set; } = UserId;
}
