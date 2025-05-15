using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Api.Rest;

public static class DiExtensions
{
    public static IServiceCollection AddEnterprise(this IServiceCollection serviceDescriptors, IConfiguration configuration)
    {
        return serviceDescriptors;
    }
}