using HC.Application.Write.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Write.Users.Command;

public class RegisterUserCommand : IRequest<OperationResult<UserWithTokenResponse>>
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }
}