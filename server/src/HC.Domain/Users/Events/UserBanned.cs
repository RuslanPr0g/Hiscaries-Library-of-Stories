using System;

namespace HC.Domain.Users.Events;

public sealed class UserBanned(Guid UserId, DateTime OccurredOn) : IDomainEvent
{
    public Guid UserId { get; } = UserId;
    public DateTime OccurredOn { get; } = OccurredOn;
}
