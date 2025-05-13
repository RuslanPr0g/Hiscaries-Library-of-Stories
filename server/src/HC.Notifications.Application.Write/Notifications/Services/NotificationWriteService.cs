using HC.Application.Write.Notifications.Command.BecomePublisher;
using HC.Application.Write.Notifications.DataAccess;

namespace HC.Application.Write.Notifications.Services;

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

    public async Task<OperationResult> ReadNotifications(ReadNotificationCommand command)
    {
        _logger.LogInformation("Attempting to read notifications for a user {UserId}", command.UserId);
        var notifications = await _repository.GetByIds(command.NotificationIds.Select(x => (NotificationId)x).ToArray());

        if (notifications.Any(x => x.UserId.Value != command.UserId))
        {
            _logger.LogWarning("User {UserId} is not owner of the provided notifications", command.UserId);
            return OperationResult.CreateClientSideError(UserFriendlyMessages.NoRights);
        }

        foreach (var notification in notifications)
        {
            notification.Read();
        }

        _logger.LogInformation("Successfully read notifications for {UserId}", command.UserId);
        return OperationResult.CreateSuccess();
    }
}
