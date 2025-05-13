namespace HC.Persistence.Context.Configurations.Converters;

public class UserAccountIdentityConverter : IdentityConverter<UserAccountId>
{
    public UserAccountIdentityConverter() :
        base((x) => new UserAccountId(x))
    {
    }
}
