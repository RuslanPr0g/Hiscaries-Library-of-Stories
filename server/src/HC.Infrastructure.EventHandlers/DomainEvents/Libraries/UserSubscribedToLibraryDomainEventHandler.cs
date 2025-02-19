﻿using HC.Application.Common.EventHandlers;
using HC.Application.Write.DataAccess;
using HC.Application.Write.PlatformUsers.DataAccess;
using HC.Domain.PlatformUsers.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Infrastructure.EventHandlers.DomainEvents.Users;

// TODO: do I want to allow one domain handler to handle multiple domain events? if no, then this approach is kinda okay
public sealed class UserSubscribedToLibraryDomainEventHandler
    : DomainEventHandler<UserSubscribedToLibraryDomainEvent>
{
    private readonly IPlatformUserWriteRepository _repository;

    public UserSubscribedToLibraryDomainEventHandler(
        IPlatformUserWriteRepository repository,
        ILogger<UserSubscribedToLibraryDomainEventHandler> logger,
        IUnitOfWork unitOfWork)
        : base(logger, unitOfWork)
    {
        _repository = repository;
    }

    protected override async Task HandleEventAsync(
        UserSubscribedToLibraryDomainEvent domainEvent,
        ConsumeContext<UserSubscribedToLibraryDomainEvent> context)
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
        library.SubscribeUser();
    }
}
