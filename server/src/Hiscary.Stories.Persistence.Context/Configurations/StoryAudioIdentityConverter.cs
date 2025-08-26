using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;
using Hiscary.Stories.Domain.Stories;

namespace Hiscary.Stories.Persistence.Context.Configurations;

public class StoryAudioIdentityConverter : IdentityConverter<StoryAudioId>
{
    public StoryAudioIdentityConverter() :
        base((x) => new StoryAudioId(x))
    {
    }
}
