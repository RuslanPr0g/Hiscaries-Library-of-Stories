﻿using Enterprise.Application.Extensions;
using HC.Media.Domain;
using HC.Media.EventHandlers.IntegrationEvents;
using HC.Media.IntegrationEvents.Incoming;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HC.Media.EventHandlers;

public static class DiExtensions
{
    public static IServiceCollection AddEventHandlers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IConsumer<ImageUploadRequestedIntegrationEvent>, ImageUploadRequestedIntegrationEventHandler>();

        services.AddBoundSettingsWithSectionAsEntityName<ResourceSettings>(configuration);

        var rabbitMqConnectionString = configuration.GetConnectionString("rabbitmq");

        services.AddMassTransit(_ =>
        {
            _.SetKebabCaseEndpointNameFormatter();
            _.SetInMemorySagaRepositoryProvider();

            var asm = Assembly.GetExecutingAssembly();

            _.AddConsumers(asm);
            _.AddSagaStateMachines(asm);
            _.AddSagas(asm);
            _.AddActivities(asm);

            _.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitMqConnectionString);
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}