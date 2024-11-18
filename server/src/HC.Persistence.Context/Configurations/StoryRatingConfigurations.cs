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
        builder.ConfigureEntity<StoryRating, StoryRatingId, StoryRatingIdentityConverter>();
        builder.Property(c => c.PlatformUserId).HasConversion(new PlatformUserIdentityConverter());
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());

        builder
            .HasOne(sr => sr.PlatformUser)
            .WithMany()
            .HasForeignKey(sr => sr.PlatformUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
