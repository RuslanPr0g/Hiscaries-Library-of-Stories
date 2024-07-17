using System;

namespace HC.Domain.Users;

public sealed record class UserRole
{
    public UserRole(int id, User user, UserRoleEnum role)
    {
        if (!Enum.IsDefined(role))
        {
            throw new ArgumentException($"Role is invalid. The provided value is {role}");
        }

        Id = id;
        User = user;
        Role = role;
    }

    public int Id { get; init; }
    public User User { get; init; }
    public UserRoleEnum Role { get; init; }

    private UserRole()
    {
    }

    public enum UserRoleEnum
    {
        Reader,
        Publisher,
        Admin
    }
}