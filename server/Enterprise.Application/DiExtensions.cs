using Enterprise.Application.Extensions;
using Enterprise.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Application;

public static class DIExtensions
{
    public static IServiceCollection AddEnterpriseApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<DbConnectionStrings>()
            .Bind(configuration.GetSection("ConnectionStrings"));

        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(jwtSettings), jwtSettings);

        var saltSettings = new SaltSettings();
        configuration.Bind(nameof(saltSettings), saltSettings);
        services.AddSingleton(saltSettings);

        services.AddHttpContextAccessor();

        services.AddJwtBearerSupport(jwtSettings);

        return services;
    }
}