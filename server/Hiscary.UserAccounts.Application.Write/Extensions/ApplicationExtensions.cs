using Hiscary.Domain.Jwt;
using Hiscary.UserAccounts.Domain;

namespace Hiscary.UserAccounts.Application.Write.Extensions;

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
