namespace HC.Persistence.Context.Configurations.Converters;

public class NotificationIdentityConverter : IdentityConverter<NotificationId>
{
    public NotificationIdentityConverter() :
        base((x) => new NotificationId(x))
    {
    }
}
