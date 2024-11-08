using HC.Application.Common.EventHandlers;
using HC.Application.Write.Users.DataAccess;
using HC.Domain.Users.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HC.Application.Solutions.NotificationHandlers;

public sealed class SolutionDomainEventHandler
    : IDomainEventHandler<UserBannedDomainEvent>
{
    private readonly IUserWriteRepository _userRepository;
    private readonly ILogger<SolutionDomainEventHandler> _logger;

    public SolutionDomainEventHandler(
        IUserWriteRepository repository,
        ILogger<SolutionDomainEventHandler> logger)
    {
        _userRepository = repository;
        _logger = logger;
    }

    // TODO: is there a way to extract this even more? so that we work only with the domain event itself, or smth...??

    public async Task Consume(ConsumeContext<UserBannedDomainEvent> context)
    {
        using (_logger.BeginScope(new { context.CorrelationId }))
        {
            _logger.LogInformation("Starting to process event {event}.", typeof(UserBannedDomainEvent));

            var user = await _userRepository.GetUserById(context.Message.UserId);

            if (user is null || !user.IsBanned)
            {
                return;
            }

            // TODO: hide all of the stories for this user

            _logger.LogInformation("Finished processing event {event}.", typeof(UserBannedDomainEvent));
        }
    }
}
