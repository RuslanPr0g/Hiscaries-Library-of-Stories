namespace Enterprise.Domain.ResultModels;

public sealed class TokenMetadata
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
