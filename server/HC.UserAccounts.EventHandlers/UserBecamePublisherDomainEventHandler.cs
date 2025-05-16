using Enterprise.EventHandlers;
using HC.PlatformUsers.Domain.Events;
using HC.UserAccounts.Domain.DataAccess;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.UserAccounts.EventHandlers;

public sealed class UserBecamePublisherDomainEventHandler(
    IUserAccountWriteRepository repository,
    ILogger<UserBecamePublisherDomainEventHandler> logger)
        : BaseEventHandler<UserBecamePublisherDomainEvent>(logger)
{
    private readonly IUserAccountWriteRepository _repository = repository;

    protected override async Task HandleEventAsync(
        UserBecamePublisherDomainEvent domainEvent,
        ConsumeContext<UserBecamePublisherDomainEvent> context)
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
