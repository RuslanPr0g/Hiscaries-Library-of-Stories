namespace HC.Persistence.Write.Configurations.Converters;

public class UserStoryBookMarkIdentityConverter : IdentityConverter<UserStoryBookMarkId>
{
    public UserStoryBookMarkIdentityConverter() :
        base((x) => new UserStoryBookMarkId(x))
    {
    }
}