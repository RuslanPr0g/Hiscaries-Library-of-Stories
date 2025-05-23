using Enterprise.Domain.ResultModels.Response;
using Microsoft.AspNetCore.Http;

namespace Enterprise.Api.Rest;

public static class ResultExtensions
{
    public static IResult OperationToHttpResult(this OperationResult? result)
    {
        if (result is null)
        {
            return Results.NotFound("Operation could not be completed as the required resource returned an empty result.");
        }

        return result.ResultStatus switch
        {
            ResultStatus.Success => Results.Ok(),
            ResultStatus.NotFound => Results.NotFound(new { result.Message }),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
            ResultStatus.ValidationError => Results.BadRequest(new { result.Message }),
            ResultStatus.ClientSideError => Results.BadRequest(new { result.Message }),
            ResultStatus.InternalServerError => Results.StatusCode(StatusCodes.Status500InternalServerError),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    public static IResult OperationToHttpResult<T>(this OperationResult<T>? result) where T : class
    {
        if (result is null)
        {
            return Results.NotFound("Operation could not be completed as the required resource returned an empty result.");
        }

        return result.ResultStatus switch
        {
            ResultStatus.Success => Results.Ok(result.Value),
            ResultStatus.NotFound => Results.NotFound(new { result.Message }),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden => Results.StatusCode(StatusCodes.Status403Forbidden),
            ResultStatus.ValidationError => Results.BadRequest(new { result.Message }),
            ResultStatus.ClientSideError => Results.BadRequest(new { result.Message }),
            ResultStatus.InternalServerError => Results.StatusCode(StatusCodes.Status500InternalServerError),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    public static IResult ToHttpResult<T>(this T? result) where T : class?
    {
        return result is null ? Results.NotFound() : Results.Ok(result);
    }
}
