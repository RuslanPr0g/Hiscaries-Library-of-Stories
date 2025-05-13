namespace HC.Stories.Persistence.Context.Configurations;

public class StoryIdentityConverter : IdentityConverter<StoryId>
{
    public StoryIdentityConverter() :
        base((x) => new StoryId(x))
    {
    }
}
