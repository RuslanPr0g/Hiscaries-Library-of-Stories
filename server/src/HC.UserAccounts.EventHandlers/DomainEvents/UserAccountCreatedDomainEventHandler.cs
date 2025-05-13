using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Infrastructure.EventHandlers.DomainEvents.Users;

// TODO: do I want to allow one domain handler to handle multiple domain events? if no, then this approach is kinda okay
public sealed class UserAccountCreatedDomainEventHandler
    : DomainEventHandler<UserAccountCreatedDomainEvent>
{
    private readonly IPlatformUserWriteRepository _repository;
    private readonly IIdGenerator _idGenerator;

    public UserAccountCreatedDomainEventHandler(
        IPlatformUserWriteRepository repository,
        ILogger<UserAccountCreatedDomainEventHandler> logger,
        IUnitOfWork unitOfWork,
        IIdGenerator idGenerator)
        : base(logger, unitOfWork)
    {
        _repository = repository;
        _idGenerator = idGenerator;
    }

    protected override async Task HandleEventAsync(
        UserAccountCreatedDomainEvent domainEvent,
        ConsumeContext<UserAccountCreatedDomainEvent> context)
    {
        var user = await _repository.GetByUserAccountId(domainEvent.UserAccountId);

        if (user is not null)
        {
            return;
        }

        // TODO: it should be domain service's responsibility to do these kind of things
        var platformUserId = _idGenerator.Generate((Guid id) => new PlatformUserId(id));
        var platformUser = new PlatformUser(platformUserId, domainEvent.UserAccountId, domainEvent.Username);

        await _repository.Add(platformUser);
    }
}
