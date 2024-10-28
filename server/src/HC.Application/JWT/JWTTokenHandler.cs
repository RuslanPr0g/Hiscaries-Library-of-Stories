using HC.Application.Options;
using HC.Application.Services;
using HC.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.JWT;

public sealed class JWTTokenHandler : IJWTTokenHandler
{
    public TokenWithClaims? GetTokenWithClaims(string token, TokenValidationParameters parameters)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        try
        {
            ClaimsPrincipal principal =
                tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

            return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : new(principal);
        }
        catch
        {
            return null;
        }
    }

    public Task<(string AccessKey, RefreshTokenDescriptor ReshreshToken)> GenerateJwtToken(User user, JwtSettings settings)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] keyInBytes = Encoding.ASCII.GetBytes(settings.Key);
        string jti = Guid.NewGuid().ToString();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, jti),
                    new Claim(JwtRegisteredClaimNames.Email, user.Username),
                    new Claim("id", user.Id.Value.ToString()),
                    new Claim("username", user.Username),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Aud, settings.Audience),
                    new Claim(JwtRegisteredClaimNames.Iss, settings.Issuer),
                }),
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.Add(settings.TokenLifeTime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(keyInBytes), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        RefreshTokenDescriptor refreshToken = new(
            token.Id,
            jti,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMonths(6),
            false,
            false);

        return Task.FromResult((tokenHandler.WriteToken(token), refreshToken));
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}