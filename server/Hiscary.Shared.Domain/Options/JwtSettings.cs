namespace Hiscary.Shared.Domain.Options;

public class JwtSettings
{
    public string Key { get; set; }
    public TimeSpan TokenLifeTime { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}