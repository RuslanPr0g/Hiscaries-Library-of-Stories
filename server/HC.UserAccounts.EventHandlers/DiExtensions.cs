using HC.PlatformUsers.IntegrationEvents.Outgoing;
using HC.UserAccounts.EventHandlers.IntegrationEvents;
using Enterprise.EventHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace HC.UserAccounts.EventHandlers;

public static class DiExtensions
{
    public static void AddEventHandlers(
        this IHostApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddScoped<IEventHandler<UserBecamePublisherIntegrationEvent>, UserBecamePublisherIntegrationEventHandler>();

        var asm = Assembly.GetExecutingAssembly();
        var rabbitMqConnectionString = configuration.GetConnectionString("rabbitmq");
        ArgumentException.ThrowIfNullOrWhiteSpace(rabbitMqConnectionString);

        builder.AddCommonEventHandlers([asm], rabbitMqConnectionString, "useraccount-events-queue");
    }
}