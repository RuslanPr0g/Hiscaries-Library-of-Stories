using System;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Users.Command.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IUserWriteService _userService;

    public CreateUserCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            AccountCreated = DateTime.Now,
            BirthDate = request.BirthDate,
            Banned = false
        };

        return await _userService.RegisterUser(user);
    }
}