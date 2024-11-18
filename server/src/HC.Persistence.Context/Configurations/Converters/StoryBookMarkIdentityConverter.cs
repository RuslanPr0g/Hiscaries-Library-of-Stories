namespace HC.Persistence.Context.Configurations.Converters;

public class StoryBookMarkIdentityConverter : IdentityConverter<StoryBookMarkId>
{
    public StoryBookMarkIdentityConverter() :
        base((x) => new StoryBookMarkId(x))
    {
    }
}