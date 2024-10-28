using HC.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HC.Application.Extensions;

public static class JwtServicesConfiguration
{
    public static IServiceCollection AddJwtBearerSupportAlongWithSwaggerSupport(
        this IServiceCollection services,
        JwtSettings jwtSettings)
    {
        services.AddJwtSupport(jwtSettings);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HC.API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement());
        });

        return services;
    }

    public static IServiceCollection AddJwtSupport(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddSingleton(jwtSettings);

        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
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
