using Enterprise.Outbox;
using HC.UserAccounts.Domain.Events;
using HC.UserAccounts.Persistence.Context;
using MassTransit;
using System.Reflection;

namespace HC.UserAccounts.Jobs;

internal class ProcessOutboxMessagesJob(
    UserAccountsContext context,
    IPublishEndpoint publisher) : BaseProcessOutboxMessagesJob<UserAccountsContext, Assembly>(publisher)
{
    protected override UserAccountsContext Context { get; init; } = context;
    protected override Assembly MessagesAssembly { get; init; } = typeof(UserAccountBannedDomainEvent).Assembly;
}
