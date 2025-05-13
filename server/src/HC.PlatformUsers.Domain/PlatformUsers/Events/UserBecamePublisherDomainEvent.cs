namespace HC.PlatformUsers.Domain.PlatformUsers.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserBecamePublisherDomainEvent(Guid PlatformUserId, Guid UserAccountId) : IDomainEvent
{
    public Guid PlatformUserId { get; } = PlatformUserId;
    public Guid UserAccountId { get; } = UserAccountId;
}
