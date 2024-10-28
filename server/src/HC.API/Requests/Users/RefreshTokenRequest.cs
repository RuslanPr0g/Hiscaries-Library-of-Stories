namespace HC.API.Requests;

public class RefreshTokenRequest
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}