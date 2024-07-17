using System;

namespace HC.Domain.Users;

public class User : Entity<UserId>, IAggregateRoot
{
    public User(int id, string username, string email, string password, DateTime accountCreated, DateTime birthDate,
        bool banned)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        AccountCreated = accountCreated;
        BirthDate = birthDate;
        Banned = banned;
    }

    public int Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public DateTime AccountCreated { get; init; }
    public DateTime BirthDate { get; init; }
    public bool Banned { get; init; }
    private User()
    {
    }
}