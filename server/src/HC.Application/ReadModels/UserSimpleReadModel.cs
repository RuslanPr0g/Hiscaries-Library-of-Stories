using HC.Domain.Users;
using System;

public class UserSimpleReadModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }

    public static UserSimpleReadModel FromDomainModel(User user)
    {
        return new()
        {
            Id = user.Id,
            Username = user.Username,
            BirthDate = user.BirthDate,
            Age = DateTime.UtcNow.Year - user.BirthDate.Year,
        };
    }
}