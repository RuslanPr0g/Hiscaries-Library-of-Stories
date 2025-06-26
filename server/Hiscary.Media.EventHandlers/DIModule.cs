using Hiscary.Media.EventHandlers.IntegrationEvents;
using Hiscary.Media.IntegrationEvents.Incoming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Enterprise.EventHandlers;
using Microsoft.Extensions.Hosting;

namespace Hiscary.Media.EventHandlers;

public static class DIModule
{
    public static void AddEventHandlers(
        this IHostApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddScoped<IEventHandler<ImageUploadRequestedIntegrationEvent>, ImageUploadRequestedIntegrationEventHandler>();

        var asm = Assembly.GetExecutingAssembly();
        var rabbitMqConnectionString = configuration.GetConnectionString("rabbitmq");
        ArgumentException.ThrowIfNullOrWhiteSpace(rabbitMqConnectionString);

        builder.AddConfigurableEventHandlers([asm], rabbitMqConnectionString, "media-events-queue");
    }
}
