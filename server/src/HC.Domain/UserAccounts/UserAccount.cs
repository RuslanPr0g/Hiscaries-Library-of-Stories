using HC.Domain.UserAccounts.Events;
using System;

namespace HC.Domain.UserAccounts;

/// <summary>
/// Represents the identity and authentication details of a user on the platform. 
/// This class handles login credentials, such as username, email, and password, 
/// and is independent of platform-specific roles or activities.
/// </summary>
public sealed class UserAccount : AggregateRoot<UserAccountId>
{
    private static string ReaderRole = "reader";
    private static string PublisherRole = "publisher";
    private static string AdminRole = "admin";

    public UserAccount(
        UserAccountId id,
        string username,
        string email,
        string password,
        DateTime birthDate) : base(id)
    {
        Username = username;
        Email = email;
        Password = password;
        BirthDate = birthDate;
        IsBanned = false;
        Role = ReaderRole;

        PublishUserCreatedEvent();
    }

    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool IsBanned { get; private set; }

    public string Role { get; private set; }

    public RefreshTokenId RefreshTokenId { get; init; }
    public RefreshToken RefreshToken { get; private set; }

    public void BecomePublisher()
    {
        Role = PublisherRole;
    }

    public void BecomeAdmin()
    {
        Role = AdminRole;
    }

    public void Ban()
    {
        if (!IsBanned)
        {
            IsBanned = true;
            PublishUserBannedEvent();
        }
    }

    public bool ValidateRefreshToken(string jwtId)
    {
        if (RefreshToken is null || RefreshToken.JwtId != jwtId)
        {
            return false;
        }

        return RefreshToken.Validate();
    }

    public void UpdateRefreshToken(RefreshToken refreshToken)
    {
        if (RefreshToken is not null)
        {
            RefreshToken.Refresh(refreshToken);
        }
        else
        {
            RefreshToken = new RefreshToken(refreshToken);
        }
    }

    public void UpdatePersonalInformation(
        string? updatedUsername,
        string? email,
        DateTime? birthDate)
    {
        if (!string.IsNullOrEmpty(updatedUsername))
        {
            Username = updatedUsername;
        }

        if (!string.IsNullOrEmpty(email))
        {
            Email = email;
        }

        if (birthDate.HasValue)
        {
            BirthDate = birthDate.Value;
        }
    }

    public void UpdatePassword(string password)
    {
        Password = password;
    }

    private void PublishUserBannedEvent()
    {
        PublishEvent(new UserAccountBannedDomainEvent(Id));
    }

    private void PublishUserCreatedEvent()
    {
        PublishEvent(new UserAccountCreatedDomainEvent(Id, Username));
    }

    private UserAccount()
    {
    }
}