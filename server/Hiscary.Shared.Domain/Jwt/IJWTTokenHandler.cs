using Hiscary.Domain.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hiscary.Domain.Jwt;

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
