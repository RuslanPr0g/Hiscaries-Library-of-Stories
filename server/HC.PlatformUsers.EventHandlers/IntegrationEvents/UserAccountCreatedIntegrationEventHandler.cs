using Enterprise.Domain.Generators;
using Enterprise.EventHandlers;
using HC.PlatformUsers.Domain;
using HC.PlatformUsers.Domain.DataAccess;
using HC.UserAccounts.IntegrationEvents.Outgoing;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.PlatformUsers.EventHandlers.IntegrationEvents;

public sealed class UserAccountCreatedIntegrationEventHandler(
    IPlatformUserWriteRepository repository,
    ILogger<UserAccountCreatedIntegrationEventHandler> logger,
    IIdGenerator idGenerator) : BaseEventHandler<UserAccountCreatedIntegrationEvent>(logger)
{
    private readonly IPlatformUserWriteRepository _repository = repository;
    private readonly IIdGenerator _idGenerator = idGenerator;

    protected override async Task HandleEventAsync(
        UserAccountCreatedIntegrationEvent domainEvent,
        ConsumeContext<UserAccountCreatedIntegrationEvent> context)
    {
        var user = await _repository.GetByUserAccountId(domainEvent.UserAccountId);

        if (user is not null)
        {
            return;
        }

        var platformUserId = _idGenerator.Generate((id) => new PlatformUserId(id));
        var platformUser = new PlatformUser(platformUserId, domainEvent.UserAccountId, domainEvent.Username);

        await _repository.Add(platformUser);
        await _repository.SaveChanges();
    }
}
