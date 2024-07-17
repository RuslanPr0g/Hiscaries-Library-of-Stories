namespace HC.Application.Models.Response;

public record UpdateUserDataResult(ResultStatus ResultStatus, string FailReason)
    : BaseResponse(ResultStatus, FailReason);