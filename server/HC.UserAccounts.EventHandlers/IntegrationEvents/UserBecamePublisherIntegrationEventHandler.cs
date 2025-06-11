using Enterprise.EventHandlers;
using HC.PlatformUsers.IntegrationEvents.Outgoing;
using HC.UserAccounts.Domain.DataAccess;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace HC.UserAccounts.EventHandlers.IntegrationEvents;

public sealed class UserBecamePublisherIntegrationEventHandler(
    IUserAccountWriteRepository repository,
    ILogger<UserBecamePublisherIntegrationEventHandler> logger)
        : IEventHandler<UserBecamePublisherIntegrationEvent>
{
    private readonly IUserAccountWriteRepository _repository = repository;

    public async Task Handle(
        UserBecamePublisherIntegrationEvent domainEvent, IMessageContext context)
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
