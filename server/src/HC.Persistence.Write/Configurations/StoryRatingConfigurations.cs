using HC.Domain.Stories;
using HC.Persistence.Write.Configurations.Converters;
using HC.Persistence.Write.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Write.Configurations;

public class StoryRatingConfigurations : IEntityTypeConfiguration<StoryRating>
{
    public void Configure(EntityTypeBuilder<StoryRating> builder)
    {
        builder.ConfigureEntity<StoryRating, StoryRatingId, StoryRatingIdentityConverter>();
        builder.Property(c => c.UserId).HasConversion(new UserIdentityConverter());
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());

        builder
            .HasOne(sr => sr.User)
            .WithMany()
            .HasForeignKey(sr => sr.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
