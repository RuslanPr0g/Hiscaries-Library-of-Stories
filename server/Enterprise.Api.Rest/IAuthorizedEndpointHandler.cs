using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public interface IAuthorizedEndpointHandler
{
    Task<IResult> WithUser<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : class?;

    IResult WithUser<TActionResult>(Func<CurrentUser, TActionResult> action)
        where TActionResult : class?;

    Task<IResult> WithUserOperation<TActionResult>(Func<CurrentUser, Task<TActionResult>> action)
        where TActionResult : OperationResult?;

    Task<IResult> WithUserOperation<TActionResultValue>(
        Func<CurrentUser, Task<OperationResult<TActionResultValue>>> action)
        where TActionResultValue : class;
}
