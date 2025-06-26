using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;
using Hiscary.Stories.Domain.Stories;

namespace Hiscary.Stories.Persistence.Context.Configurations;

public class StoryIdentityConverter : IdentityConverter<StoryId>
{
    public StoryIdentityConverter() :
        base((x) => new StoryId(x))
    {
    }
}
