using System;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Users.Command.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, RegisterUserResult>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<RegisterUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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