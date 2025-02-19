﻿using HC.Application.Write.FileStorage;
using HC.Application.Write.Generators;
using HC.Application.Write.ImageCompressors;
using HC.Application.Write.ImageUploaderss;
using HC.Application.Write.JWT;
using HC.Application.Write.Notifications.Services;
using HC.Application.Write.PlatformUsers.Services;
using HC.Application.Write.Stories.Services;
using HC.Application.Write.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Application.Write;

public static class DIExtensions
{
    public static IServiceCollection AddApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddSingleton<IIdGenerator, IdGenerator>();

        services.AddScoped<IPlatformUserWriteService, PlatformUserWriteService>();
        services.AddScoped<IUserAccountWriteService, UserAccountWriteService>();
        services.AddScoped<IStoryWriteService, StoryWriteService>();
        services.AddScoped<INotificationWriteService, NotificationWriteService>();
        services.AddScoped<IJWTTokenHandler, JWTTokenHandler>();

        services.AddScoped<IImageCompressor, DefaultImageCompressor>();
        services.AddScoped<IImageUploader, ImageUploader>();

        services.AddHttpContextAccessor();
        services.AddScoped<IResourceUrlGeneratorService, ResourceUrlGeneratorService>();
        services.AddSingleton<IFileStorageService>(new LocalFileStorageService("wwwroot"));

        return services;
    }
}