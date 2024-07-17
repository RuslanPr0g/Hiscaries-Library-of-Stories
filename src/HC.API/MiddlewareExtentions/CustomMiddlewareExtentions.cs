using HC.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace HC.API.MiddlewareExtentions;

public static class CustomMiddlewareExtentions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<CustomMiddleware>();
    }
}