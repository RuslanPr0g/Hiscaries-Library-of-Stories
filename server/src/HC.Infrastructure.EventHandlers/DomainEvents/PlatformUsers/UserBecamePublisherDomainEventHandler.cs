﻿using HC.Application.Common.EventHandlers;
using HC.Application.Write.DataAccess;
using HC.Application.Write.UserAccounts.DataAccess;
using HC.Domain.PlatformUsers.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Infrastructure.EventHandlers.DomainEvents.Users;

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
