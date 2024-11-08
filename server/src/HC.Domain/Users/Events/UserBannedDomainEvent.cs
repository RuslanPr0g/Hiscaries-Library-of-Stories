using System;

namespace HC.Domain.Users.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserBannedDomainEvent(Guid UserId) : IDomainEvent
{
    public Guid UserId { get; } = UserId;
}
