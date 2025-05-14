using Enterprise.Persistence.Context.Configurations;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Persistence.Context.Configurations;

public class StoryAudioIdentityConverter : IdentityConverter<StoryAudioId>
{
    public StoryAudioIdentityConverter() :
        base((x) => new StoryAudioId(x))
    {
    }
}
