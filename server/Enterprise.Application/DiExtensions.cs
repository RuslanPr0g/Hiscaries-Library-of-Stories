using Enterprise.Application.Extensions;
using Enterprise.Domain.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Application;

public static class DIExtensions
{
    public static WebApplicationBuilder AddEnterpriseApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<DbConnectionStrings>()
            .Bind(builder.Configuration.GetSection("ConnectionStrings"));

        var jwtSettings = new JwtSettings();
        builder.Configuration.Bind(nameof(jwtSettings), jwtSettings);

        var saltSettings = new SaltSettings();
        builder.Configuration.Bind(nameof(saltSettings), saltSettings);
        builder.Services.AddSingleton(saltSettings);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddJwtBearerSupport(jwtSettings);

        return builder;
    }
}