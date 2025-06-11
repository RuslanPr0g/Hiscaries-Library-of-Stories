using HC.Media.EventHandlers.IntegrationEvents;
using HC.Media.IntegrationEvents.Incoming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Enterprise.EventHandlers;
using Microsoft.Extensions.Hosting;

namespace HC.Media.EventHandlers;

public static class DiExtensions
{
    public static void AddEventHandlers(
        this IHostApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddScoped<IEventHandler<ImageUploadRequestedIntegrationEvent>, ImageUploadRequestedIntegrationEventHandler>();

        var asm = Assembly.GetExecutingAssembly();
        var rabbitMqConnectionString = configuration.GetConnectionString("rabbitmq");
        ArgumentException.ThrowIfNullOrWhiteSpace(rabbitMqConnectionString);

        builder.AddCommonEventHandlers([asm], rabbitMqConnectionString, "media-events-queue");
    }
}
