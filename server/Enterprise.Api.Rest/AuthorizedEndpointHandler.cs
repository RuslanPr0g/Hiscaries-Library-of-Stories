using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public class AuthorizedEndpointHandler(HttpContext context) : IAuthorizedEndpointHandler
{
    private readonly HttpContext _context = context;

    public async Task<IResult> WithUser<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : class?
    {
        var user = CurrentUser.FromClaims(_context.User);

        if (user is null)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = await action(user);
        return result.ToHttpResult();
    }
}
