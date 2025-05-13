using Enterprise.Domain;

namespace HC.UserAccounts.Domain.Events;

public sealed class UserAccountCreatedDomainEvent(Guid UserAccountId, string Username) : IDomainEvent
{
    public Guid UserAccountId { get; } = UserAccountId;
    public string Username { get; set; } = Username;
}
