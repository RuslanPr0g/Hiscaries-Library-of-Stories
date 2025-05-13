using HC.Notifications.Application.Read.Notifications.DataAccess;
using HC.Notifications.Application.Read.Notifications.ReadModels;

namespace HC.Notifications.Application.Read.Notifications.Services;

public sealed class NotificationReadService : INotificationReadService
{
    private readonly INotificationReadRepository _repository;

    public NotificationReadService(INotificationReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NotificationReadModel>> GetNotifications(UserAccountId userId) =>
        await _repository.GetNotificationsByUserId(userId);
}
