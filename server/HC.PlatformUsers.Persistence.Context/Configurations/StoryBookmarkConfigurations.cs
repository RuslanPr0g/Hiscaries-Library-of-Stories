using Enterprise.Persistence.Context.Extensions;
using HC.PlatformUsers.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.PlatformUsers.Persistence.Context.Configurations;

public class StoryBookmarkConfigurations : IEntityTypeConfiguration<StoryBookMark>
{
    public void Configure(EntityTypeBuilder<StoryBookMark> builder)
    {
        builder.ToTable("StoryBookMarks");
        builder.ConfigureEntity();
        builder.HasKey(sp => new { sp.StoryId, sp.PlatformUserId });
        builder.Property(c => c.StoryId).IsRequired();
        builder.Property(c => c.PlatformUserId).HasConversion(new PlatformUserIdentityConverter());
    }
}
