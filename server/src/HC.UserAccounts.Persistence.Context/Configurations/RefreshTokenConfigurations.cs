namespace HC.UserAccounts.Persistence.Context.Configurations;

public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.ConfigureEntity<RefreshToken, RefreshTokenId, RefreshTokenIdentityConverter>();
    }
}
