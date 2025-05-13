namespace HC.PlatformUsers.Domain.PlatformUsers.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserSubscribedToLibraryDomainEvent(Guid SubscriberUserAccountId, Guid LibraryId) : IDomainEvent
{
    public Guid SubscriberUserAccountId { get; } = SubscriberUserAccountId;
    public Guid LibraryId { get; } = LibraryId;
}
