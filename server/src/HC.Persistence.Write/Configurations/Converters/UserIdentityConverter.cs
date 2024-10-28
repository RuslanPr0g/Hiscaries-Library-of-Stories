using HC.Domain.Users;

namespace HC.Persistence.Write.Configurations.Converters;

public class UserIdentityConverter : IdentityConverter<UserId>
{
    public UserIdentityConverter() :
        base((x) => new UserId(x))
    {
    }
}
