using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Outbox;

public static class DiExtensions
{
    public static IServiceCollection AddEnterprise(this IServiceCollection serviceDescriptors, IConfiguration configuration)
    {
        return serviceDescriptors;
    }
}