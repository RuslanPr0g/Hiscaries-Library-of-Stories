namespace HC.UserAccounts.Api.Rest.Requests;

public class RefreshTokenRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}