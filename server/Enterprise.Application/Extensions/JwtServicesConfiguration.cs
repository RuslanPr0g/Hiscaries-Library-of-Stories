using Enterprise.Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Enterprise.Application.Extensions;

public static class JwtServicesConfiguration
{
    public static IServiceCollection AddJwtBearerSupport(
        this IServiceCollection services,
        JwtSettings jwtSettings)
    {
        services.AddJwtSupport(jwtSettings);
        return services;
    }

    public static IServiceCollection AddJwtSupport(
        this IServiceCollection services,
        JwtSettings jwtSettings)
    {
        services.AddSingleton(jwtSettings);

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = tokenValidationParameters;

             options.Authority = "Authority URL";

             var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
             var logger = loggerFactory.CreateLogger<JwtBearerEvents>();

             options.Events = new JwtBearerEvents
             {
                 OnMessageReceived = SetUpSignalRAuth,
                 OnAuthenticationFailed = context =>
                 {
                     logger.LogDebug("Authentication failed: {Message}", context.Exception.Message);
                     return Task.CompletedTask;
                 },
                 OnTokenValidated = context =>
                 {
                     logger.LogDebug("Token validated successfully.");
                     return Task.CompletedTask;
                 }
             };
         });

        return services;
    }

    private static Task SetUpSignalRAuth(MessageReceivedContext context)
    {
        var accessToken = context.Request.Query["access_token"];

        var path = context.HttpContext.Request.Path;
        if (!string.IsNullOrEmpty(accessToken) &&
            path.StartsWithSegments("/hubs"))
        {
            context.Token = accessToken;
        }
        return Task.CompletedTask;
    }
}
