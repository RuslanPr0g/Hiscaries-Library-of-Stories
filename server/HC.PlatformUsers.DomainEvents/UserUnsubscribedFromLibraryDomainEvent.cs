using Enterprise.Events;

namespace HC.PlatformUsers.DomainEvents;

public sealed class UserUnsubscribedFromLibraryDomainEvent(
    Guid SubscriberUserAccountId,
    Guid LibraryId) : BaseDomainEvent
{
    public Guid SubscriberUserAccountId { get; } = SubscriberUserAccountId;
    public Guid LibraryId { get; } = LibraryId;
}
