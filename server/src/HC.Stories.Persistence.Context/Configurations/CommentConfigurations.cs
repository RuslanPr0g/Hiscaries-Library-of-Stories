namespace HC.Stories.Persistence.Context.Configurations;

public class CommentConfigurations : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.ConfigureEntity<Comment, CommentId, CommentIdentityConverter>();
        builder.Property(c => c.StoryId).HasConversion(new StoryIdentityConverter());
        builder.Property(c => c.PlatformUserId).HasConversion(new PlatformUserIdentityConverter());

        builder
            .HasOne(c => c.PlatformUser)
            .WithMany()
            .HasForeignKey(c => c.PlatformUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
