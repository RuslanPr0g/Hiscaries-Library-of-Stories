using HC.Notifications.Domain.DataAccess;
using HC.Notifications.Domain.ReadModels;
using HC.Notifications.Domain.Services;

namespace HC.Notifications.Application.Read.Services;

public sealed class NotificationReadService(INotificationReadRepository repository) : INotificationReadService
{
    private readonly INotificationReadRepository _repository = repository;

    public async Task<IEnumerable<NotificationReadModel>> GetNotifications(Guid userAccountId) =>
        await _repository.GetNotificationsByUserId(userAccountId);
}
