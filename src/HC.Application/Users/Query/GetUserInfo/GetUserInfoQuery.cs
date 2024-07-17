using HC.Application.DTOs;
using MediatR;

namespace HC.Application.Users.Query;

public class GetUserInfoQuery : IRequest<UserReadDto>
{
    public string Username { get; set; }
}