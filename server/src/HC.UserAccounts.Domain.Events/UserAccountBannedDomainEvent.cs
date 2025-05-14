using Enterprise.Domain;

namespace HC.UserAccounts.Domain.Events;

public sealed class UserAccountBannedDomainEvent(Guid UserId) : IDomainEvent
{
    public Guid UserId { get; } = UserId;
}
