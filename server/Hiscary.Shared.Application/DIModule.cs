using Hiscary.Application.Extensions;
using Hiscary.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.Application;

public static class DIModule
{
    public static IServiceCollection AddSharedApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<DbConnectionStrings>()
            .Bind(configuration.GetSection("ConnectionStrings"));

        services.AddBoundSettingsWithSectionAsEntityName<JwtSettings>(configuration, out var jwtSettings);
        services.AddBoundSettingsWithSectionAsEntityName<SaltSettings>(configuration, out var saltSettings);

        services.AddSingleton(saltSettings);

        services.AddHttpContextAccessor();

        services.AddJwtBearerSupport(jwtSettings);

        return services;
    }
}