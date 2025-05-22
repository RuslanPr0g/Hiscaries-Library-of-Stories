using Enterprise.EventHandlers;
using HC.PlatformUsers.IntegrationEvents.Outgoing;
using HC.UserAccounts.Domain.DataAccess;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.UserAccounts.EventHandlers.IntegrationEvents;

public sealed class UserBecamePublisherIntegrationEventHandler(
    IUserAccountWriteRepository repository,
    ILogger<UserBecamePublisherIntegrationEventHandler> logger)
        : BaseEventHandler<UserBecamePublisherIntegrationEvent>(logger)
{
    private readonly IUserAccountWriteRepository _repository = repository;

    protected override async Task HandleEventAsync(
        UserBecamePublisherIntegrationEvent domainEvent,
        ConsumeContext<UserBecamePublisherIntegrationEvent> context)
    {
        var user = await _repository.GetById(domainEvent.UserAccountId);

        if (user is null)
        {
            return;
        }

        user.BecomePublisher();

        await _repository.SaveChanges();
    }
}
