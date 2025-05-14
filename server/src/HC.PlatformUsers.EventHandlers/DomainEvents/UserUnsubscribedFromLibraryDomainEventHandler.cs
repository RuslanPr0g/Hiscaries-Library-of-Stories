using Enterprise.EventHandlers;
using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.PlatformUsers.EventHandlers.DomainEvents;

public sealed class UserUnsubscribedFromLibraryDomainEventHandler(
    IPlatformUserWriteRepository repository,
    ILogger<UserUnsubscribedFromLibraryDomainEventHandler> logger)
        : BaseEventHandler<UserUnsubscribedFromLibraryDomainEvent>(logger)
{
    private readonly IPlatformUserWriteRepository _repository = repository;

    protected override async Task HandleEventAsync(
        UserUnsubscribedFromLibraryDomainEvent domainEvent,
        ConsumeContext<UserUnsubscribedFromLibraryDomainEvent> context)
    {
        var user = await _repository.GetLibraryOwnerByLibraryId(domainEvent.LibraryId);

        if (user is null)
        {
            return;
        }

        var library = user.GetCurrentLibrary();

        if (library is null)
        {
            return;
        }

        library.UnsubscribeUser();

        await _repository.SaveChanges();
    }
}
