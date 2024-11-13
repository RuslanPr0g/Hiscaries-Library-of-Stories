using HC.Domain.Users;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class UserReadHistoryConfigurations : IEntityTypeConfiguration<ReadingHistory>
{
    public void Configure(EntityTypeBuilder<ReadingHistory> builder)
    {
        builder.ConfigureEntity();
        builder.HasKey(sp => new { sp.StoryId, sp.UserId });
        builder.Property(c => c.UserId).HasConversion(new UserIdentityConverter());
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());
    }
}
