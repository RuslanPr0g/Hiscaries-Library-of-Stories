using HC.Application.Options;
using HC.Application.Services;
using HC.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace HC.Application.JWT;

public interface IJWTTokenHandler
{
    TokenWithClaims? GetTokenWithClaims(string token, TokenValidationParameters parameters);
    Task<(string AccessKey, RefreshTokenDescriptor ReshreshToken)> GenerateJwtToken(User user, JwtSettings settings);
}
