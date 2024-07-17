namespace HC.Application.Models.Response;

public record UpdateUserRoleResult(ResultStatus ResultStatus, string FailReason)
    : BaseResponse(ResultStatus, FailReason);