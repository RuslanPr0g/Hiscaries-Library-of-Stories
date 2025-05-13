namespace HC.Stories.Persistence.Context.Configurations;

public class StoryAudioIdentityConverter : IdentityConverter<StoryAudioId>
{
    public StoryAudioIdentityConverter() :
        base((x) => new StoryAudioId(x))
    {
    }
}
