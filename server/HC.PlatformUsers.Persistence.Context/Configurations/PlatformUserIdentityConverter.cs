using Enterprise.Persistence.Context.Configurations;

namespace HC.PlatformUsers.Persistence.Context.Configurations;

public class PlatformUserIdentityConverter : IdentityConverter<PlatformUserId>
{
    public PlatformUserIdentityConverter() :
        base((x) => new PlatformUserId(x))
    {
    }
}
