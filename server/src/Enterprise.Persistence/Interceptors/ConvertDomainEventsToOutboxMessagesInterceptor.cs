using Enterprise.Domain;
using Enterprise.Domain.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Enterprise.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor
    : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync
        (DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? context = eventData.Context;

        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var messages = context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(root =>
            {
                var domainEvents = root.DomainEvents;

                root.ClearEvents();

                return domainEvents;
            })
            .Select(x => new OutboxMessage()
            {
                Id = Guid.NewGuid(),
                OccuredOnUtc = DateTime.UtcNow,
                Type = x.GetType().FullName ?? x.GetType().Name,
                Content = JsonConvert.SerializeObject(x, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(messages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
