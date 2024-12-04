﻿using HC.Domain.PlatformUsers;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class PlatformUserConfigurations : IEntityTypeConfiguration<PlatformUser>
{
    public void Configure(EntityTypeBuilder<PlatformUser> builder)
    {
        builder.ToTable("PlatformUsers");
        builder.ConfigureEntity<PlatformUser, PlatformUserId, PlatformUserIdentityConverter>();
        builder.Property(c => c.UserAccountId).HasConversion(new UserAccountIdentityConverter());
        builder.HasIndex(x => x.UserAccountId)
            .HasDatabaseName("IX_UserAccountId")
            .IsUnique();

        builder
            .HasMany(u => u.Reviews)
            .WithOne()
            .HasForeignKey(r => r.PlatformUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(u => u.Bookmarks)
            .WithOne()
            .HasForeignKey(b => b.PlatformUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.ReadHistory)
            .WithOne()
            .HasForeignKey(rh => rh.PlatformUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(s => s.Libraries)
            .WithOne(s => s.PlatformUser)
            .HasForeignKey(sp => sp.PlatformUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
