using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public interface IAuthorizedEndpointHandler
{
    public Task<IResult> WithUser<TActionResult>(
        Func<CurrentUser, Task<TActionResult>> action,
        bool shouldValidateUserId = false,
        bool shouldValidationUsername = false,
        bool shouldValidateEntireUser = true)
        where TActionResult : class;
}
