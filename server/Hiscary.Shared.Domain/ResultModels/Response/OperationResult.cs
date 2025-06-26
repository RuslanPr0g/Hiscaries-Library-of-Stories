using Hiscary.Shared.Domain.ResultModels.Response;

public record OperationResult(ResultStatus ResultStatus, string? Message = null)
{
    public static OperationResult CreateSuccess() => new(ResultStatus.Success);

    public static OperationResult CreateClientSideError(string message) => Error(ResultStatus.ClientSideError, message);
    public static OperationResult CreateServerSideError(string message) => Error(ResultStatus.InternalServerError, message);
    public static OperationResult CreateNotFound(string message) => Error(ResultStatus.NotFound, message);
    public static OperationResult CreateValidationsError(string message) => Error(ResultStatus.ValidationError, message);
    public static OperationResult CreateUnauthorizedError(string message) => Error(ResultStatus.Unauthorized, message);

    private static OperationResult Error(ResultStatus status, string message) => new(status, message);
}

public record OperationResult<T>(ResultStatus ResultStatus, T? Value = default, string? Message = null)
    where T : class
{
    public static OperationResult<T> CreateSuccess(T value) => new(ResultStatus.Success, value);

    public static OperationResult<T> CreateClientSideError(string message) => Error(ResultStatus.ClientSideError, message);
    public static OperationResult<T> CreateServerSideError(string message) => Error(ResultStatus.InternalServerError, message);
    public static OperationResult<T> CreateNotFound(string message) => Error(ResultStatus.NotFound, message);
    public static OperationResult<T> CreateValidationsError(string message) => Error(ResultStatus.ValidationError, message);
    public static OperationResult<T> CreateUnauthorizedError(string message) => Error(ResultStatus.Unauthorized, message);

    private static OperationResult<T> Error(ResultStatus status, string message) => new(status, default, message);
}
