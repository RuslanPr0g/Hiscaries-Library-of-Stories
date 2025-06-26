using Hiscary.UserAccounts.Domain;
using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;

namespace Hiscary.UserAccounts.Persistence.Context.Configurations;

public class UserAccountIdentityConverter : IdentityConverter<UserAccountId>
{
    public UserAccountIdentityConverter() :
        base((x) => new UserAccountId(x))
    {
    }
}
