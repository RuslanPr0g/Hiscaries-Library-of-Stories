﻿using Enterprise.Persistence.Context.Extensions;
using HC.UserAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.UserAccounts.Persistence.Context.Configurations;

public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.ConfigureEntity<RefreshToken, RefreshTokenId, RefreshTokenIdentityConverter>();
    }
}
