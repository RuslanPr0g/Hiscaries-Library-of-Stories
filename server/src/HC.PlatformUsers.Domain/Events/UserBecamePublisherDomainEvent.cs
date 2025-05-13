using Enterprise.Domain;

namespace HC.PlatformUsers.Domain.Events;

public sealed class UserBecamePublisherDomainEvent(
    Guid PlatformUserId,
    Guid UserAccountId) : IDomainEvent
{
    public Guid PlatformUserId { get; } = PlatformUserId;
    public Guid UserAccountId { get; } = UserAccountId;
}
