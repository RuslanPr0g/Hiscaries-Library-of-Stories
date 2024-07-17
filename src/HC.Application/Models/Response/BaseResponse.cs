namespace HC.Application.Models.Response;

public record BaseResponse(ResultStatus ResultStatus, string FailReason);