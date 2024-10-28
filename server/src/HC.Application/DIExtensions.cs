using HC.Application.FileStorage;
using HC.Application.Generators;
using HC.Application.JWT;
using HC.Application.Stories.Services;
using HC.Application.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Application;

public static class DIExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IIdGenerator, IdGenerator>();

        services.AddScoped<IUserWriteService, UserWriteService>();
        services.AddScoped<IStoryWriteService, StoryWriteService>();
        services.AddScoped<IUserReadService, UserReadService>();
        services.AddScoped<IStoryReadService, StoryReadService>();
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();

        services.AddHttpContextAccessor();
        services.AddScoped<IResourceUrlGeneratorService, ResourceUrlGeneratorService>();
        services.AddSingleton<IFileStorageService>(new LocalFileStorageService("wwwroot"));

        return services;
    }
}