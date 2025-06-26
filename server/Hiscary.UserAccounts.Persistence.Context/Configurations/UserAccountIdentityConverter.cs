using Hiscary.Persistence.Context.Configurations;
using Hiscary.UserAccounts.Domain;

namespace Hiscary.UserAccounts.Persistence.Context.Configurations;

public class UserAccountIdentityConverter : IdentityConverter<UserAccountId>
{
    public UserAccountIdentityConverter() :
        base((x) => new UserAccountId(x))
    {
    }
}
