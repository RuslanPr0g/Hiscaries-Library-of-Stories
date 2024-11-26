using System;

namespace HC.Domain.PlatformUsers.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserUnsubscribedFromLibraryDomainEvent(Guid PlatformUserId, Guid LibraryId) : IDomainEvent
{
    public Guid PlatformUserId { get; } = PlatformUserId;
    public Guid LibraryId { get; } = LibraryId;
}
