using HC.Application.Common.EventHandlers;
using HC.Application.Write.DataAccess;
using HC.Application.Write.Generators;
using HC.Application.Write.Notifications.DataAccess;
using HC.Application.Write.PlatformUsers.DataAccess;
using HC.Domain.Notifications;
using HC.Domain.Stories.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Infrastructure.EventHandlers.DomainEvents.Users;

// TODO: do I want to allow one domain handler to handle multiple domain events? if no, then this approach is kinda okay
public sealed class StoryPublishedDomainEventHandler
    : DomainEventHandler<StoryPublishedDomainEvent>
{
    private readonly IPlatformUserWriteRepository _platformUserRepository;
    private readonly INotificationWriteRepository _notificationRepository;
    private readonly IIdGenerator _idGenerator;

    public StoryPublishedDomainEventHandler(
        IPlatformUserWriteRepository repository,
        ILogger<StoryPublishedDomainEventHandler> logger,
        IUnitOfWork unitOfWork,
        IIdGenerator idGenerator,
        INotificationWriteRepository notificationRepository)
        : base(logger, unitOfWork)
    {
        _platformUserRepository = repository;
        _idGenerator = idGenerator;
        _notificationRepository = notificationRepository;
    }

    protected override async Task HandleEventAsync(StoryPublishedDomainEvent domainEvent, ConsumeContext<StoryPublishedDomainEvent> context)
    {
        // TODO: we need to realize a batch here, as a library can have millions of users...
        // also we do not need to make it acid, if some notification fails, other should be saved anyway...

        var userIds = await _platformUserRepository.GetLibrarySubscribersUserAccountIds(domainEvent.LibraryId);

        foreach (var userId in userIds)
        {
            var notificationId = _idGenerator.Generate((val) => new NotificationId(val));
            var notification = new Notification(notificationId, userId, $"Story was published: {domainEvent.Title}", "StoryPublished");
            await _notificationRepository.Add(notification);
        }
    }
}
