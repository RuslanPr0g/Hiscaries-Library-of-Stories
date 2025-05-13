using HC.Persistence.Context.Configurations.Converters;

namespace HC.Persistence.Context.Configurations;

public class StoryBookmarkConfigurations : IEntityTypeConfiguration<StoryBookMark>
{
    public void Configure(EntityTypeBuilder<StoryBookMark> builder)
    {
        builder.ToTable("StoryBookMarks");
        builder.ConfigureEntity();
        builder.HasKey(sp => new { sp.StoryId, sp.PlatformUserId });
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());
        builder.Property(c => c.PlatformUserId).HasConversion(new PlatformUserIdentityConverter());
    }
}
