using HC.Media.IntegrationEvents.Outgoing;
using HC.PlatformUsers.EventHandlers.IntegrationEvents;
using HC.Stories.IntegrationEvents.Outgoing;
using HC.UserAccounts.IntegrationEvents.Outgoing;
using Enterprise.EventHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.Extensions.Hosting;

namespace HC.PlatformUsers.EventHandlers;

public static class DiExtensions
{
    public static void AddEventHandlers(
        this IHostApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddScoped<IEventHandler<StoryPublishedIntegrationEvent>, StoryPublishedIntegrationEventHandler>();
        builder.Services.AddScoped<IEventHandler<UserAccountCreatedIntegrationEvent>, UserAccountCreatedIntegrationEventHandler>();
        builder.Services.AddScoped<IEventHandler<ImageUploadedIntegrationEvent>, ImageUploadedIntegrationEventHandler>();

        var asm = Assembly.GetExecutingAssembly();
        var rabbitMqConnectionString = configuration.GetConnectionString("rabbitmq");
        ArgumentException.ThrowIfNullOrWhiteSpace(rabbitMqConnectionString);

        builder.AddCommonEventHandlers([asm], rabbitMqConnectionString, "platformuser-events-queue");
    }
}