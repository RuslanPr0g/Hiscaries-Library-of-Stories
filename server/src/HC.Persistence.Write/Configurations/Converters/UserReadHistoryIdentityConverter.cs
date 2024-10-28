using HC.Domain.Users;

namespace HC.Persistence.Write.Configurations.Converters;

public class UserReadHistoryIdentityConverter : IdentityConverter<UserReadHistoryId>
{
    public UserReadHistoryIdentityConverter() :
        base((x) => new UserReadHistoryId(x))
    {
    }
}
