using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.PlatformUsers.EventHandlers.DomainEvents;

// TODO: do I want to allow one domain handler to handle multiple domain events? if no, then this approach is kinda okay
public sealed class UserBecamePublisherDomainEventHandler
    : DomainEventHandler<UserBecamePublisherDomainEvent>
{
    private readonly IUserAccountWriteRepository _repository;

    public UserBecamePublisherDomainEventHandler(
        IUserAccountWriteRepository repository,
        ILogger<UserBecamePublisherDomainEventHandler> logger,
        IUnitOfWork unitOfWork)
        : base(logger, unitOfWork)
    {
        _repository = repository;
    }

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
    }
}
