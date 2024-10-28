using HC.Application.Write.Options;
using HC.Application.Write.Tokens;
using HC.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace HC.Application.Write.JWT;

public interface IJWTTokenHandler
{
    TokenWithClaims? GetTokenWithClaims(string token, TokenValidationParameters parameters);
    Task<(string AccessKey, RefreshTokenDescriptor ReshreshToken)> GenerateJwtToken(User user, JwtSettings settings);
}
