using Enterprise.Persistence.Context.Configurations;

namespace HC.PlatformUsers.Persistence.Context.Configurations;

public class ReviewIdentityConverter : IdentityConverter<ReviewId>
{
    public ReviewIdentityConverter() :
        base((x) => new ReviewId(x))
    {
    }
}
