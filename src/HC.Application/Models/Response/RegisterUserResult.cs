namespace HC.Application.Models.Response;

public record RegisterUserResult(ResultStatus ResultStatus, string FailReason, string Token, string RefreshToken)
    : BaseResponse(ResultStatus, FailReason);