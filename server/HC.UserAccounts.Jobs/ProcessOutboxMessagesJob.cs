using Enterprise.Domain.EventPublishers;
using Enterprise.Outbox;
using HC.UserAccounts.IntegrationEvents;
using HC.UserAccounts.Persistence.Context;
using System.Reflection;

namespace HC.UserAccounts.Jobs;

internal class ProcessOutboxMessagesJob(
    UserAccountsContext context,
    IEventPublisher publisher) : BaseProcessOutboxMessagesJob<UserAccountsContext, Assembly>(publisher)
{
    protected override UserAccountsContext Context { get; init; } = context;
    protected override IReadOnlyList<Assembly> MessagesAssembly { get; init; } = 
        [typeof(UserAccountIntegrationEventsAssembly).Assembly];
}
