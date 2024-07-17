﻿using System;

namespace HC.Domain.Users;

public sealed class RefreshToken : Entity<RefreshTokenId>
{
    public RefreshToken(
        RefreshTokenId id,
        string jwtId,
        string token,
        DateTime creationDate,
        DateTime expiryDate,
        bool used,
        bool invalidated) : base(id)
    {
        JwtId = jwtId;
        Token = token;
        CreationDate = creationDate;
        ExpiryDate = expiryDate;
        Used = used;
        Invalidated = invalidated;
    }

    public string JwtId { get; init; }
    public string Token { get; init; }
    public DateTime CreationDate { get; init; }
    public DateTime ExpiryDate { get; init; }
    public bool Used { get; init; }
    public bool Invalidated { get; init; }

    private RefreshToken()
    {
    }
}