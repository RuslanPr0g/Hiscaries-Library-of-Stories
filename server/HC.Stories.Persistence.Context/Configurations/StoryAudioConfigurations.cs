using Enterprise.Persistence.Context.Extensions;
using HC.Stories.Domain.Stories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Stories.Persistence.Context.Configurations;

public class StoryAudioConfigurations : IEntityTypeConfiguration<StoryAudio>
{
    public void Configure(EntityTypeBuilder<StoryAudio> builder)
    {
        builder.ToTable("StoryAudios");
        builder.ConfigureEntity<StoryAudio, StoryAudioId, StoryAudioIdentityConverter>();
    }
}
