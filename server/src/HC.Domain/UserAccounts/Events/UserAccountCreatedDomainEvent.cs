using System;

namespace HC.Domain.UserAccounts.Events;

// TODO: maybe record is not the best choice for domain event representation?
public sealed class UserAccountCreatedDomainEvent(Guid UserAccountId, string Username) : IDomainEvent
{
    public Guid UserAccountId { get; } = UserAccountId;
    public string Username { get; set; } = Username;
}
