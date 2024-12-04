using HC.Domain.Stories;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class StoryRatingConfigurations : IEntityTypeConfiguration<StoryRating>
{
    public void Configure(EntityTypeBuilder<StoryRating> builder)
    {
        builder.ToTable("StoryRatings");
        builder.ConfigureEntity();
        builder.HasKey(sp => new { sp.StoryId, sp.PlatformUserId });
        builder.Property(c => c.PlatformUserId).HasConversion(new PlatformUserIdentityConverter());
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());

        builder
            .HasOne(sr => sr.PlatformUser)
            .WithMany()
            .HasForeignKey(sr => sr.PlatformUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
