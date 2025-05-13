namespace HC.Domain.PlatformUsers.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserUnsubscribedFromLibraryDomainEvent(Guid SubscriberUserAccountId, Guid LibraryId) : IDomainEvent
{
    public Guid SubscriberUserAccountId { get; } = SubscriberUserAccountId;
    public Guid LibraryId { get; } = LibraryId;
}
