namespace Enterprise.Domain.ResultModels.Response;

// TODO: is it too bad? maybe look up for best practices?

public record OperationResult(ResultStatus ResultStatus, string? Message = null)
{
    public static OperationResult CreateSuccess()
    {
        return new OperationResult(ResultStatus.Success);
    }

    public static OperationResult CreateClientSideError(string message)
    {
        return new OperationResult(ResultStatus.ClientSideError, message);
    }

    public static OperationResult CreateServerSideError(string message)
    {
        return new OperationResult(ResultStatus.InternalServerError, message);
    }

    public static OperationResult CreateNotFound(string message)
    {
        return new OperationResult(ResultStatus.NotFound, message);
    }

    public static OperationResult CreateValidationsError(string message)
    {
        return new OperationResult(ResultStatus.ValidationError, message);
    }

    public static OperationResult CreateUnauthorizedError(string message)
    {
        return new OperationResult(ResultStatus.Unauthorized, message);
    }
}

public record OperationResult<T>(ResultStatus ResultStatus, T? Value, string? Message = null)
    where T : class
{
    public static OperationResult<T> CreateSuccess(T value)
    {
        return new OperationResult<T>(ResultStatus.Success, value);
    }

    public static OperationResult<T> CreateClientSideError(string message)
    {
        return new OperationResult<T>(ResultStatus.ClientSideError, default, message);
    }

    public static OperationResult<T> CreateServerSideError(string message)
    {
        return new OperationResult<T>(ResultStatus.InternalServerError, default, message);
    }

    public static OperationResult<T> CreateNotFound(string message)
    {
        return new OperationResult<T>(ResultStatus.NotFound, default, message);
    }

    public static OperationResult<T> CreateValidationsError(string message)
    {
        return new OperationResult<T>(ResultStatus.ValidationError, default, message);
    }
}
