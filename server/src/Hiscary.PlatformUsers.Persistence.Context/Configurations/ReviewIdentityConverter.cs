using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;

namespace Hiscary.PlatformUsers.Persistence.Context.Configurations;

public class ReviewIdentityConverter : IdentityConverter<ReviewId>
{
    public ReviewIdentityConverter() :
        base((x) => new ReviewId(x))
    {
    }
}
