namespace Enterprise.Application.ResultModels.Response;
public enum ResultStatus
{
    Success,
    NotFound,
    Unauthorized,
    Forbidden,
    ValidationError,
    ClientSideError,
    InternalServerError
}
