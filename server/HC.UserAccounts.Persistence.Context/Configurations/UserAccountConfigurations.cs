﻿using Enterprise.Persistence.Context.Extensions;
using HC.UserAccounts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.UserAccounts.Persistence.Context.Configurations;

public class UserAccountConfigurations : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("UserAccounts");
        builder.ConfigureEntity<UserAccount, UserAccountId, UserAccountIdentityConverter>();
        builder.Property(c => c.RefreshTokenId).HasConversion(new RefreshTokenIdentityConverter());

        builder
            .HasOne(u => u.RefreshToken)
            .WithOne()
            .HasForeignKey<UserAccount>(u => u.RefreshTokenId);
    }
}
