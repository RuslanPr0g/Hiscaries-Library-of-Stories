﻿using Hiscary.Stories.Domain.Stories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StackNucleus.DDD.Persistence.EF.Postgres.Extensions;

namespace Hiscary.Stories.Persistence.Context.Configurations;

public class CommentConfigurations : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.ConfigureEntity<Comment, CommentId, CommentIdentityConverter>();
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());
        builder.Property(c => c.PlatformUserId).IsRequired();
    }
}
