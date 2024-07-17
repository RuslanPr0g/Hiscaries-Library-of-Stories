using System;

namespace HC.Domain.Users;

public sealed record class RefreshToken
{
    public RefreshToken(int id, string jwtId, string token, DateTime creationDate, DateTime expiryDate, bool used,
        bool invalidated, int userId)
    {
        Id = id;
        JwtId = jwtId;
        Token = token;
        CreationDate = creationDate;
        ExpiryDate = expiryDate;
        Used = used;
        Invalidated = invalidated;
        UserId = userId;
    }

    public int Id { get; init; }
    public string JwtId { get; init; }
    public string Token { get; init; }
    public DateTime CreationDate { get; init; }
    public DateTime ExpiryDate { get; init; }
    public bool Used { get; init; }
    public bool Invalidated { get; init; }
    public int UserId { get; init; }
}