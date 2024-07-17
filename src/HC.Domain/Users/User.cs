﻿using Enterprise.Domain;
using System;

namespace HC.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    public User(
        UserId id,
        string username,
        string email,
        string password,
        DateTime accountCreated,
        DateTime birthDate,
        bool banned,
        RefreshTokenId refreshTokenId) : base(id)
    {
        Username = username;
        Email = email;
        Password = password;
        AccountCreated = accountCreated;
        BirthDate = birthDate;
        Banned = banned;
        RefreshTokenId = refreshTokenId;
    }

    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public DateTime AccountCreated { get; init; }
    public DateTime BirthDate { get; init; }
    public bool Banned { get; init; }

    public RefreshTokenId RefreshTokenId { get; init; }
    public RefreshToken RefreshToken { get; init; }

    private User()
    {
    }
}