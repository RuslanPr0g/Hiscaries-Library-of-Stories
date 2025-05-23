using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public class AuthorizedEndpointHandler(IHttpContextAccessor contextAccessor) : IAuthorizedEndpointHandler
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public async Task<IResult> WithUser<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : class?
    {
        var context = _contextAccessor.HttpContext;

        if (context is null)
        {
            return Results.BadRequest("Something went wrong when processing the http request.");
        }

        var user = CurrentUser.FromClaims(context.User);

        if (user is null)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = await action(user);
        return result.ToHttpResult();
    }

    public IResult WithUser<TActionResult>(Func<CurrentUser, TActionResult> action)
        where TActionResult : class?
    {
        var context = _contextAccessor.HttpContext;

        if (context is null)
        {
            return Results.BadRequest("Something went wrong when processing the http request.");
        }

        var user = CurrentUser.FromClaims(context.User);

        if (user is null)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = action(user);
        return result.ToHttpResult();
    }

    public async Task<IResult> WithUserOperation<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : OperationResult?
    {
        var context = _contextAccessor.HttpContext;

        if (context is null)
        {
            return Results.BadRequest("Something went wrong when processing the http request.");
        }

        var user = CurrentUser.FromClaims(context.User);

        if (user is null)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = await action(user);
        return result.OperationToHttpResult();
    }

    public IResult WithUserOperation<TActionResult>(Func<CurrentUser, TActionResult> action)
        where TActionResult : OperationResult?
    {
        var context = _contextAccessor.HttpContext;

        if (context is null)
        {
            return Results.BadRequest("Something went wrong when processing the http request.");
        }

        var user = CurrentUser.FromClaims(context.User);

        if (user is null)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = action(user);
        return result.OperationToHttpResult();
    }
}
