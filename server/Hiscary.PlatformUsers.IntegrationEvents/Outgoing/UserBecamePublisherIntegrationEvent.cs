using Hiscary.Events;

namespace Hiscary.PlatformUsers.IntegrationEvents.Outgoing;

public sealed class UserBecamePublisherIntegrationEvent(
    Guid PlatformUserId,
    Guid UserAccountId) : BaseIntegrationEvent
{
    public Guid PlatformUserId { get; } = PlatformUserId;
    public Guid UserAccountId { get; } = UserAccountId;
}
