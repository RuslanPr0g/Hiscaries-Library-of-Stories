using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public class AuthorizedEndpointHandler(HttpContext context) : IAuthorizedEndpointHandler
{
    private readonly HttpContext _context = context;

    public async Task<IResult> WithUser<TActionResult>(
        Func<CurrentUser, Task<TActionResult>> action,
        bool shouldValidateUserId = false,
        bool shouldValidationUsername = false,
        bool shouldValidateEntireUser = true)
        where TActionResult : class
    {
        var user = CurrentUser.FromClaims(_context.User);

        if (shouldValidateUserId && user.IsIdEmpty)
        {
            return Results.BadRequest("Unable to interpret the provided token's user id.");
        }

        if (shouldValidationUsername && user.IsUsernameEmpty)
        {
            return Results.BadRequest("Unable to interpret the provided token's user username.");
        }

        if (shouldValidateEntireUser && user.IsUserEmpty)
        {
            return Results.BadRequest("Unable to interpret the provided token.");
        }

        var result = await action(user);
        return result.ToHttpResult();
    }
}
