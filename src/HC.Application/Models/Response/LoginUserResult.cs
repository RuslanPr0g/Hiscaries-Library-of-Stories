namespace HC.Application.Models.Response;

public record LoginUserResult(ResultStatus ResultStatus, string FailReason, string Token, string RefreshToken)
    : BaseResponse(ResultStatus, FailReason);