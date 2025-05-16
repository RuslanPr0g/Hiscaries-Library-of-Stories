using Enterprise.Persistence.Context.Configurations;
using HC.UserAccounts.Domain;

namespace HC.UserAccounts.Persistence.Context.Configurations;

public class UserAccountIdentityConverter : IdentityConverter<UserAccountId>
{
    public UserAccountIdentityConverter() :
        base((x) => new UserAccountId(x))
    {
    }
}
