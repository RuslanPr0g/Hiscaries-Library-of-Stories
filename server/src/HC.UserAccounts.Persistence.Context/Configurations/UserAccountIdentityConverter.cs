namespace HC.UserAccounts.Persistence.Context.Configurations;

public class UserAccountIdentityConverter : IdentityConverter<UserAccountId>
{
    public UserAccountIdentityConverter() :
        base((x) => new UserAccountId(x))
    {
    }
}
