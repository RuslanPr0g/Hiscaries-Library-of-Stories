using Enterprise.EventHandlers;
using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.DomainEvents;
using Microsoft.Extensions.Logging;
using Wolverine;

namespace HC.PlatformUsers.EventHandlers.DomainEvents;

public sealed class UserUnsubscribedFromLibraryDomainEventHandler(
    IPlatformUserWriteRepository repository,
    ILogger<UserUnsubscribedFromLibraryDomainEventHandler> logger)
        : IEventHandler<UserUnsubscribedFromLibraryDomainEvent>
{
    private readonly IPlatformUserWriteRepository _repository = repository;

    public async Task Handle(
        UserUnsubscribedFromLibraryDomainEvent domainEvent, IMessageContext context)
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
