using StackNucleus.DDD.Persistence.EF.Postgres.Configurations;
using Hiscary.Notifications.Domain;

namespace Hiscary.Notifications.Persistence.Context.Configurations;

public class NotificationIdentityConverter : IdentityConverter<NotificationId>
{
    public NotificationIdentityConverter() :
        base((x) => new NotificationId(x))
    {
    }
}
