namespace HC.Application.Models.Response;
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
