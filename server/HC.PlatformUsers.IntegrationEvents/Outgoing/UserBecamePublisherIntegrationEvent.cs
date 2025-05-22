using Enterprise.Events;

namespace HC.PlatformUsers.IntegrationEvents.Outgoing;

public sealed class UserBecamePublisherIntegrationEvent(
    Guid PlatformUserId,
    Guid UserAccountId) : BaseIntegrationEvent
{
    public Guid PlatformUserId { get; } = PlatformUserId;
    public Guid UserAccountId { get; } = UserAccountId;
}
