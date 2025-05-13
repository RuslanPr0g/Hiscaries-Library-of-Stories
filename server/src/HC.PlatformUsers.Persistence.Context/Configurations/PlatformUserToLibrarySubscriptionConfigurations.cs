using HC.Persistence.Context.Configurations.Converters;

namespace HC.Persistence.Context.Configurations;

public class PlatformUserToLibrarySubscriptionConfigurations : IEntityTypeConfiguration<PlatformUserToLibrarySubscription>
{
    public void Configure(EntityTypeBuilder<PlatformUserToLibrarySubscription> builder)
    {
        builder.ToTable("PlatformUserToLibrarySubscription");
        builder.ConfigureEntity();
        builder.HasKey(sp => new { sp.LibraryId, sp.PlatformUserId });
        builder.Property(c => c.LibraryId).HasConversion(new LibraryIdentityConverter());
        builder.Property(c => c.PlatformUserId).HasConversion(new PlatformUserIdentityConverter());

        builder
            .HasOne(uls => uls.PlatformUser)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(uls => uls.PlatformUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
