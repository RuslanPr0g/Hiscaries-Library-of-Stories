using Hiscary.UserAccounts.Application.Write.Services;
using Hiscary.UserAccounts.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hiscary.UserAccounts.Application.Write;

public static class DIModule
{
    public static IServiceCollection AddUserAccountsApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserAccountWriteService, UserAccountWriteService>();
        return services;
    }
}