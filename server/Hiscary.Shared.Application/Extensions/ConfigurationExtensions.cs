using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Application.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddBoundSettingsWithSectionAsEntityName<TSettings>(
        this IServiceCollection services,
        IConfiguration configuration,
        out TSettings settings,
        string? sectionName = null)
        where TSettings : class, new()
    {
        if (sectionName is null)
        {
            sectionName = typeof(TSettings).Name;
        }

        settings = new TSettings();
        configuration.Bind(sectionName, settings);
        services.AddSingleton(settings);
        return services;
    }

    public static IServiceCollection AddBoundSettingsWithSectionAsEntityName<TSettings>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = null)
        where TSettings : class, new()
    {
        return services.AddBoundSettingsWithSectionAsEntityName<TSettings>(
            configuration,
            out var _,
            sectionName);
    }
}