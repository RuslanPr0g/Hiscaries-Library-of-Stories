namespace HC.UserAccounts.Persistence.Context.Configurations;

public class RefreshTokenIdentityConverter : IdentityConverter<RefreshTokenId>
{
    public RefreshTokenIdentityConverter() :
        base((x) => new RefreshTokenId(x))
    {
    }
}
