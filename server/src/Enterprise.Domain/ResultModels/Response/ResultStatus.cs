namespace Enterprise.Domain.ResultModels;
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
