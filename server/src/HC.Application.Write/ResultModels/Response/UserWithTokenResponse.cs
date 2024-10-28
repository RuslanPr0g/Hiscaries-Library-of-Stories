namespace HC.Application.Write.ResultModels.Response;

public sealed class UserWithTokenResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
