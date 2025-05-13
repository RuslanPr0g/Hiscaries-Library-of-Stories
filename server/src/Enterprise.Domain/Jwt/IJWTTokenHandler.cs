using Enterprise.Domain.Jwt;
using Enterprise.Domain.Options;
using Microsoft.IdentityModel.Tokens;

namespace Enterprise.Jwt;

public interface IJWTTokenHandler
{
    TokenWithClaims? GetTokenWithClaims(string token, TokenValidationParameters parameters);
    Task<(string AccessKey, RefreshTokenDescriptor ReshreshToken)> GenerateJwtToken(
        Guid id,
        string email,
        string username,
        string role,
        JwtSettings settings);
}
