using Enterprise.EventHandlers;
using Enterprise.Generators;
using HC.PlatformUsers.Domain;
using HC.PlatformUsers.Domain.DataAccess;
using HC.UserAccounts.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.PlatformUsers.EventHandlers.IntegrationEvents;

public sealed class UserAccountCreatedIntegrationEventHandler(
    IPlatformUserWriteRepository repository,
    ILogger<UserAccountCreatedIntegrationEventHandler> logger,
    IIdGenerator idGenerator) : BaseEventHandler<UserAccountCreatedDomainEvent>(logger)
{
    private readonly IPlatformUserWriteRepository _repository = repository;
    private readonly IIdGenerator _idGenerator = idGenerator;

    protected override async Task HandleEventAsync(
        UserAccountCreatedDomainEvent domainEvent,
        ConsumeContext<UserAccountCreatedDomainEvent> context)
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
