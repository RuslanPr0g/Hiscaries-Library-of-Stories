using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;

namespace Hiscary.UserAccounts.Persistence.Context.Configurations;

public class RefreshTokenIdentityConverter : IdentityConverter<RefreshTokenId>
{
    public RefreshTokenIdentityConverter() :
        base((x) => new RefreshTokenId(x))
    {
    }
}
