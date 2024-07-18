namespace HC.Application.ResultModels.Response;

public sealed class UserWithTokenResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
