using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public interface IAuthorizedEndpointHandler
{
    public Task<IResult> WithUser<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : class?;

    public IResult WithUser<TActionResult>(Func<CurrentUser, TActionResult> action)
        where TActionResult : class?;
}
