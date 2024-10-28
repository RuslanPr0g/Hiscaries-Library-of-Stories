using HC.Domain.Stories;

namespace HC.Persistence.Context.Configurations.Converters;

public class StoryRatingIdentityConverter : IdentityConverter<StoryRatingId>
{
    public StoryRatingIdentityConverter() :
        base((x) => new StoryRatingId(x))
    {
    }
}
