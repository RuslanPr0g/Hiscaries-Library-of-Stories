using HC.Domain.Stories;

namespace HC.Persistence.Write.Configurations.Converters;

public class StoryIdentityConverter : IdentityConverter<StoryId>
{
    public StoryIdentityConverter() :
        base((x) => new StoryId(x))
    {
    }
}
