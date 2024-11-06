using HC.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HC.Application.Extensions;

public static class JwtServicesConfiguration
{
    public static IServiceCollection AddJwtBearerSupport(
        this IServiceCollection services,
        JwtSettings jwtSettings)
    {
        services.AddJwtSupport(jwtSettings);
        return services;
    }

    public static IServiceCollection AddJwtSupport(this IServiceCollection services, JwtSettings jwtSettings)
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

             var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
             var logger = loggerFactory.CreateLogger<JwtBearerEvents>();

             options.Events = new JwtBearerEvents
             {
                 OnAuthenticationFailed = context =>
                 {
                     logger.LogError("Authentication failed: {Message}", context.Exception.Message);
                     return Task.CompletedTask;
                 },
                 OnTokenValidated = context =>
                 {
                     logger.LogInformation("Token validated successfully.");
                     return Task.CompletedTask;
                 }
             };
         });

        return services;
    }
}
