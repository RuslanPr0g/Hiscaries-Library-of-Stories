namespace HC.Persistence.Context.Configurations.Converters;

public class PlatformUserIdentityConverter : IdentityConverter<PlatformUserId>
{
    public PlatformUserIdentityConverter() :
        base((x) => new PlatformUserId(x))
    {
    }
}
