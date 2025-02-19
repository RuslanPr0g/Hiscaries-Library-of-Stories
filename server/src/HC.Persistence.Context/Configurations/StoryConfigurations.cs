﻿using HC.Domain.Stories;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class StoryConfigurations : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.ToTable("Stories");
        builder.ConfigureEntity<Story, StoryId, StoryIdentityConverter>();
        builder.Property(c => c.LibraryId).HasConversion(new LibraryIdentityConverter());

        builder
            .HasOne(s => s.Library)
            .WithMany()
            .HasForeignKey(s => s.LibraryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(s => s.Genres)
            .WithMany()
            .UsingEntity(j => j.ToTable("StoryGenres"));

        builder
            .HasMany(s => s.Contents)
            .WithOne()
            .HasForeignKey(sp => sp.StoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(s => s.Comments)
            .WithOne(c => c.Story)
            .HasForeignKey(c => c.StoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(s => s.Audios)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(s => s.Ratings)
            .WithOne()
            .HasForeignKey(sr => sr.StoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(s => s.ReadHistory)
            .WithOne()
            .HasForeignKey(sr => sr.StoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Genres).AutoInclude();
        builder.Navigation(x => x.Library).AutoInclude();
        builder.Navigation(x => x.Ratings).AutoInclude();
        builder.Navigation(x => x.ReadHistory).AutoInclude();
        builder.Navigation(x => x.Comments).AutoInclude();
        builder.Navigation(x => x.Contents).AutoInclude();
    }
}
