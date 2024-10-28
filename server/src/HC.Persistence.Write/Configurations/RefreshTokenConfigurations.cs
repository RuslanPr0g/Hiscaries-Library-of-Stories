using HC.Domain.Users;
using HC.Persistence.Write.Configurations.Converters;
using HC.Persistence.Write.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Write.Configurations;

public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ConfigureEntity<RefreshToken, RefreshTokenId, RefreshTokenIdentityConverter>();
    }
}
