using Hiscary.Persistence.Context.Configurations;

namespace Hiscary.PlatformUsers.Persistence.Context.Configurations;

public class PlatformUserIdentityConverter : IdentityConverter<PlatformUserId>
{
    public PlatformUserIdentityConverter() :
        base((x) => new PlatformUserId(x))
    {
    }
}
