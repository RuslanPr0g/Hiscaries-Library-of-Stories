using Enterprise.Domain.Generators;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Generators;

public static class DIExtensions
{
    public static IServiceCollection AddEnterpriseGenerators(this IServiceCollection services)
    {
        services.AddSingleton<IIdGenerator, IdGenerator>();

        services.AddHttpContextAccessor();

        return services;
    }
}