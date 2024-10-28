using HC.Domain.Users;

namespace HC.Persistence.Context.Configurations.Converters;

public class UserReadHistoryIdentityConverter : IdentityConverter<UserReadHistoryId>
{
    public UserReadHistoryIdentityConverter() :
        base((x) => new UserReadHistoryId(x))
    {
    }
}
