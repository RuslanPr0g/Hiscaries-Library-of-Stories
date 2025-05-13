namespace HC.Domain.UserAccounts.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserAccountBannedDomainEvent(Guid UserId) : IDomainEvent
{
    public Guid UserId { get; } = UserId;
}
