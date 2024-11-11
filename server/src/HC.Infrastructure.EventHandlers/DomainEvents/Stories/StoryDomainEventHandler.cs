using HC.Application.Common.EventHandlers;
using HC.Application.Write.DataAccess;
using HC.Application.Write.Stories.DataAccess;
using HC.Domain.Users.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Infrastructure.EventHandlers.DomainEvents.Users;

public sealed class StoryDomainEventHandler
    : IDomainEventHandler<StoryPageReadDomainEvent>
{
    private readonly IStoryWriteRepository _repository;
    private readonly ILogger<StoryDomainEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public StoryDomainEventHandler(
        IStoryWriteRepository repository,
        ILogger<StoryDomainEventHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    // TODO: is there a way to extract this even more? so that we work only with the domain event itself, or smth...??

    public async Task Consume(ConsumeContext<StoryPageReadDomainEvent> context)
    {
        using (_logger.BeginScope(new { context.CorrelationId }))
        {
            _logger.LogInformation("Starting to process event {event}.", typeof(StoryPageReadDomainEvent));

            var story = await _repository.GetStory(context.Message.StoryId);

            if (story is null)
            {
                return;
            }

            story.UpdateTitle($"{context.Message.Page} | {story.Title}");

            await _unitOfWork.SaveChanges();

            _logger.LogInformation("Finished processing event {event}.", typeof(StoryPageReadDomainEvent));
        }
    }
}
