namespace HC.Stories.Persistence.Context.Configurations;

public class StoryAudioConfigurations : IEntityTypeConfiguration<StoryAudio>
{
    public void Configure(EntityTypeBuilder<StoryAudio> builder)
    {
        builder.ToTable("StoryAudios");
        builder.ConfigureEntity<StoryAudio, StoryAudioId, StoryAudioIdentityConverter>();
    }
}
