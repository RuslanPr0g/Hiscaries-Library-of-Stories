using HC.Application.Options;
using HC.Application.Tokens;
using HC.Domain.UserAccounts;
using Microsoft.IdentityModel.Tokens;

namespace HC.Application.Write.JWT;

public interface IJWTTokenHandler
{
    TokenWithClaims? GetTokenWithClaims(string token, TokenValidationParameters parameters);
    Task<(string AccessKey, RefreshTokenDescriptor ReshreshToken)> GenerateJwtToken(UserAccount user, JwtSettings settings);
}
