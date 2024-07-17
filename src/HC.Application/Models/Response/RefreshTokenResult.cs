namespace HC.Application.Models.Response;

public record RefreshTokenResponse(ResultStatus ResultStatus, string FailReason, string Token, string RefreshToken)
    : BaseResponse(ResultStatus, FailReason);