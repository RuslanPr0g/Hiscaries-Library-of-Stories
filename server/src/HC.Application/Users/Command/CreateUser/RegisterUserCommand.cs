using HC.Application.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Users.Command;

public class RegisterUserCommand : IRequest<OperationResult<UserWithTokenResponse>>
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }
}