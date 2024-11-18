using System;

namespace HC.Domain.UserAccounts;

public sealed record UserAccountId(Guid Value) : Identity(Value)
{
    public static implicit operator UserAccountId(Guid identity) => new(identity);
    public static implicit operator Guid(UserAccountId identity) => identity.Value;
}
