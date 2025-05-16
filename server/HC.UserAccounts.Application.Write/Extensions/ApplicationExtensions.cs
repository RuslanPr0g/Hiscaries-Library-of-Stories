using Enterprise.Domain.Jwt;
using HC.UserAccounts.Domain;

namespace HC.UserAccounts.Application.Write.Extensions;

internal static class ApplicationExtensions
{
    public static RefreshToken ToDomainModel(this RefreshTokenDescriptor descriptor, RefreshTokenId id) => new(
        id,
        descriptor.Token,
        descriptor.JwtId,
        descriptor.CreationDate,
        descriptor.ExpiryDate,
        descriptor.Used,
        descriptor.Invalidated);
}
