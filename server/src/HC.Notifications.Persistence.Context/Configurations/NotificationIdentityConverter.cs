using Enterprise.Persistence.Context.Configurations;
using HC.Notifications.Domain.Notifications;

namespace HC.Notifications.Persistence.Context.Configurations;

public class NotificationIdentityConverter : IdentityConverter<NotificationId>
{
    public NotificationIdentityConverter() :
        base((x) => new NotificationId(x))
    {
    }
}
