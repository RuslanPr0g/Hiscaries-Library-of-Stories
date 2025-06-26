using Hiscary.Persistence.Context.Extensions;
using Hiscary.Notifications.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiscary.Notifications.Persistence.Context.Configurations;

public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");
        builder.ConfigureEntity<Notification, NotificationId, NotificationIdentityConverter>();
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Type).IsRequired();
        builder.Property(c => c.Message).IsRequired();
        builder.Property(c => c.IsRead).IsRequired();
        builder.Property(c => c.RelatedObjectId);
        builder.Property(c => c.PreviewUrl);
    }
}
