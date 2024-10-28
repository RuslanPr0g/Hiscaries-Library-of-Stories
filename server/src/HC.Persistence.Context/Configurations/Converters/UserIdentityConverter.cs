using HC.Domain.Users;

namespace HC.Persistence.Context.Configurations.Converters;

public class UserIdentityConverter : IdentityConverter<UserId>
{
    public UserIdentityConverter() :
        base((x) => new UserId(x))
    {
    }
}
