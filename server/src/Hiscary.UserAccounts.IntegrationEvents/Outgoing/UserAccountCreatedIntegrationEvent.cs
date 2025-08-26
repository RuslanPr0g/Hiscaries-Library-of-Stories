using StackNucleus.DDD.Domain.Events;

namespace Hiscary.UserAccounts.IntegrationEvents.Outgoing;

public sealed class UserAccountCreatedIntegrationEvent(
    Guid UserAccountId,
    string Username) :
    BaseIntegrationEvent
{
    public Guid UserAccountId { get; } = UserAccountId;
    public string Username { get; set; } = Username;
}
