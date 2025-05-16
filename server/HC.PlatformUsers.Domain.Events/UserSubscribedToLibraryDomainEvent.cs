using Enterprise.Domain;

namespace HC.PlatformUsers.Domain.Events;

public sealed class UserSubscribedToLibraryDomainEvent(
    Guid SubscriberUserAccountId,
    Guid LibraryId) : IDomainEvent
{
    public Guid SubscriberUserAccountId { get; } = SubscriberUserAccountId;
    public Guid LibraryId { get; } = LibraryId;
}
