using Enterprise.Domain.Constants;
using Hiscary.Notifications.Domain;
using Hiscary.Notifications.Domain.DataAccess;
using Hiscary.Notifications.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Hiscary.Notifications.Application.Write.Services;

public sealed class NotificationWriteService : INotificationWriteService
{
    private readonly INotificationWriteRepository _repository;
    private readonly ILogger<NotificationWriteService> _logger;

    public NotificationWriteService(
        INotificationWriteRepository repository,
        ILogger<NotificationWriteService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<OperationResult> ReadNotifications(Guid userId, Guid[] notificationIds)
    {
        _logger.LogInformation("Attempting to read notifications for a user {UserId}", userId);
        var notifications = await _repository
            .GetByIds(notificationIds.Select(_ => (NotificationId)_).ToArray());

        if (notifications.Any(x => x.UserId != userId))
        {
            _logger.LogWarning("User {UserId} is not owner of the provided notifications", userId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.NoRights);
        }

        foreach (var notification in notifications)
        {
            notification.Read();
        }

        await _repository.SaveChanges();

        _logger.LogInformation("Successfully read notifications for {UserId}", userId);
        return OperationResult.CreateSuccess();
    }
}
