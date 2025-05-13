using Enterprise.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Outbox;

public static class DiExtensions
{
    public static IServiceCollection AddEnterprise(this IServiceCollection serviceDescriptors, IConfiguration configuration)
    {
        serviceDescriptors.AddEnterpriseOutbox();
        serviceDescriptors.AddEnterprisePersistenceContext(configuration);
        return serviceDescriptors;
    }
}