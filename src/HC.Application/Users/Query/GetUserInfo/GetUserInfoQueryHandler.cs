using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HC.Application.DTOs;
using HC.Application.Interface;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Users.Query;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserReadDto>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetUserInfoQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<UserReadDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        User user = await _userService.GetUserByUsername(request.Username);

        UserReadDto mappedUsers = _mapper.Map<UserReadDto>(user);

        if (mappedUsers is null) return null;

        mappedUsers.BirthDate = user.BirthDate.ToString("MM/dd/yyyy");

        (int tr, int ts, double asc) = await _userService.GetAdvancedInfoByUsername(request.Username);

        mappedUsers.Role = await _userService.GetUserRoleByUsername(request.Username);
        mappedUsers.TotalReads = tr;
        mappedUsers.TotalStories = ts;
        mappedUsers.AverageScore = asc;

        return mappedUsers;
    }
}