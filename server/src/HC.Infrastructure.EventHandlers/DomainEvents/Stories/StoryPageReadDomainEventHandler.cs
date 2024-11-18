﻿using HC.Application.Common.EventHandlers;
using HC.Application.Write.DataAccess;
using HC.Application.Write.Stories.DataAccess;
using HC.Domain.PlatformUsers.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Infrastructure.EventHandlers.DomainEvents.Users;

// TODO: do I want to allow one domain handler to handle multiple domain events? if no, then this approach is kinda okay
public sealed class StoryPageReadDomainEventHandler
    : DomainEventHandler<StoryPageReadDomainEvent>
{
    private readonly IStoryWriteRepository _repository;

    public StoryPageReadDomainEventHandler(
        IStoryWriteRepository repository,
        ILogger<StoryPageReadDomainEventHandler> logger,
        IUnitOfWork unitOfWork)
        : base(logger, unitOfWork)
    {
        _repository = repository;
    }

    protected override async Task HandleEventAsync(StoryPageReadDomainEvent domainEvent, ConsumeContext<StoryPageReadDomainEvent> context)
    {
        var story = await _repository.GetStory(domainEvent.StoryId);
        if (story is null)
        {
            return;
        }

        story.UpdateAgeLimit(domainEvent.Page);
    }
}