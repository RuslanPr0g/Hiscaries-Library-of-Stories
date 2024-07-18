#nullable enable

using HC.Application.Models.Response;
using Microsoft.AspNetCore.Http;

namespace HC.API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToResult(this OperationResult result)
        {
            return result.ResultStatus switch
            {
                ResultStatus.Success => Results.Ok(),
                ResultStatus.NotFound => Results.NotFound(),
                ResultStatus.Unauthorized => Results.Unauthorized(),
                ResultStatus.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
                ResultStatus.ValidationError => Results.BadRequest(result.Message),
                ResultStatus.ClientSideError => Results.BadRequest(result.Message),
                ResultStatus.InternalServerError => Results.StatusCode(StatusCodes.Status500InternalServerError),
                _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        public static IResult ToResult<T>(this OperationResult<T> result) where T : class
        {
            return result.ResultStatus switch
            {
                ResultStatus.Success => Results.Ok(result.Value),
                ResultStatus.NotFound => Results.NotFound(),
                ResultStatus.Unauthorized => Results.Unauthorized(),
                ResultStatus.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
                ResultStatus.ValidationError => Results.BadRequest(result.Message),
                ResultStatus.ClientSideError => Results.BadRequest(result.Message),
                ResultStatus.InternalServerError => Results.StatusCode(StatusCodes.Status500InternalServerError),
                _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
            };
        }

        public static IResult ToResult<T>(this T? result) where T : class
        {
            return result == null ? Results.NotFound() : Results.Ok(result);
        }
    }
}
