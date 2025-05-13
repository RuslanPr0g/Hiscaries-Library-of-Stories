using System.ComponentModel.DataAnnotations.Schema;

namespace HC.Domain.UserAccounts;

[NotMapped]
public sealed record UserAccountId(Guid Value) : Identity(Value)
{
    public static implicit operator UserAccountId(Guid identity) => new(identity);
    public static implicit operator Guid(UserAccountId identity) => identity.Value;
}
