using Enterprise.Persistence.Context.Configurations;
using Hiscary.Stories.Domain.Stories;

namespace Hiscary.Stories.Persistence.Context.Configurations;

public class StoryIdentityConverter : IdentityConverter<StoryId>
{
    public StoryIdentityConverter() :
        base((x) => new StoryId(x))
    {
    }
}
