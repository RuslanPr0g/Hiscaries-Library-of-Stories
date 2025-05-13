using HC.Persistence.Context.Configurations.Converters;

namespace HC.Persistence.Context.Configurations;

public class StoryAudioConfigurations : IEntityTypeConfiguration<StoryAudio>
{
    public void Configure(EntityTypeBuilder<StoryAudio> builder)
    {
        builder.ToTable("StoryAudios");
        builder.ConfigureEntity<StoryAudio, StoryAudioId, StoryAudioIdentityConverter>();
    }
}
