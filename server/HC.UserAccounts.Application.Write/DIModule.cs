using HC.UserAccounts.Application.Write.Services;
using HC.UserAccounts.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HC.UserAccounts.Application.Write;

public static class DIModule
{
    public static IServiceCollection AddUserAccountsApplicationWriteLayer(this IServiceCollection services)
    {
        services.AddScoped<IUserAccountWriteService, UserAccountWriteService>();
        return services;
    }
}