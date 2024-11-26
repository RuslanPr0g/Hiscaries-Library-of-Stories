using HC.Domain.Notifications;
using HC.Persistence.Context.Configurations.Converters;
using HC.Persistence.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Persistence.Context.Configurations;

public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ConfigureEntity<Notification, NotificationId, NotificationIdentityConverter>();
        builder.Property(c => c.UserId).HasConversion(new UserAccountIdentityConverter());
    }
}
