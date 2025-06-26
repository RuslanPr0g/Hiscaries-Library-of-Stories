using Hiscary.Events;

namespace Hiscary.PlatformUsers.DomainEvents;

public sealed class UserSubscribedToLibraryDomainEvent(
    Guid SubscriberUserAccountId,
    Guid LibraryId) : BaseDomainEvent
{
    public Guid SubscriberUserAccountId { get; } = SubscriberUserAccountId;
    public Guid LibraryId { get; } = LibraryId;
}
