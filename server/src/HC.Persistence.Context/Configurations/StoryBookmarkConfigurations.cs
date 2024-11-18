using HC.Domain.PlatformUsers;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class StoryBookmarkConfigurations : IEntityTypeConfiguration<StoryBookMark>
{
    public void Configure(EntityTypeBuilder<StoryBookMark> builder)
    {
        builder.ConfigureEntity<StoryBookMark, StoryBookMarkId, StoryBookMarkIdentityConverter>();
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());
        builder.Property(c => c.PlatformUserId).HasConversion(new PlatformUserIdentityConverter());
    }
}
