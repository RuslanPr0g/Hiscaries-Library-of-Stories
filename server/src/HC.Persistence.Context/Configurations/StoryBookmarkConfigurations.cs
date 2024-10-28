using HC.Domain.Users;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class StoryBookmarkConfigurations : IEntityTypeConfiguration<UserStoryBookMark>
{
    public void Configure(EntityTypeBuilder<UserStoryBookMark> builder)
    {
        builder.ConfigureEntity<UserStoryBookMark, UserStoryBookMarkId, UserStoryBookMarkIdentityConverter>();
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());
        builder.Property(c => c.UserId).HasConversion(new UserIdentityConverter());
    }
}
