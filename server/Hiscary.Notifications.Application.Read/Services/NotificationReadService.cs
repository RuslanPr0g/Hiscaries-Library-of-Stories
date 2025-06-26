using Hiscary.Notifications.Domain.DataAccess;
using Hiscary.Notifications.Domain.ReadModels;
using Hiscary.Notifications.Domain.Services;

namespace Hiscary.Notifications.Application.Read.Services;

public sealed class NotificationReadService(INotificationReadRepository repository) : INotificationReadService
{
    private readonly INotificationReadRepository _repository = repository;

    public async Task<IEnumerable<NotificationReadModel>> GetNotifications(Guid userAccountId) =>
        await _repository.GetNotificationsByUserId(userAccountId);
}
