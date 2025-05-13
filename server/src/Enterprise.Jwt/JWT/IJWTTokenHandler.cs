namespace HC.Application.Write.JWT;

public interface IJWTTokenHandler
{
    TokenWithClaims? GetTokenWithClaims(string token, TokenValidationParameters parameters);
    Task<(string AccessKey, RefreshTokenDescriptor ReshreshToken)> GenerateJwtToken(UserAccount user, JwtSettings settings);
}
