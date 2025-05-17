using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public abstract class BaseApiEndpointHandler
{
    protected async Task<IResult> WithUser(HttpContext context, Func<CurrentUser, Task<IResult>> action)
    {
        var user = CurrentUser.FromClaims(context.User);
        if (user.HasNoData)
            return Results.BadRequest("Unable to interpret the provided token.");

        var result = await action(user);
        return result.ToHttpResult();
    }
}
