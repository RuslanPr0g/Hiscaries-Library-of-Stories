using System;
using HC.Application.Models.Connection;
using HC.Application.Models.Response;
using MediatR;

namespace HC.Application.Users.Command;

public class UpdateUserDataCommand : IRequest<UpdateUserDataResult>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string UsernameUpdate { get; set; }
    public bool Banned { get; set; } = false;
    public DateTime BirthDate { get; set; }

    public string PreviousPassword { get; set; }
    public string NewPassword { get; set; }
}