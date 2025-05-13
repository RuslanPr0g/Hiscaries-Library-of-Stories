using HC.Application.Extensions;
using HC.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Application.Common;

public static class DIExtensions
{
    public static WebApplicationBuilder AddApplicationCommonLayer(this WebApplicationBuilder builder)
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