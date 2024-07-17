using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HC.Application.Interface;
using MediatR;

namespace HC.Application.Users.Query.BecomePublisher;

public class BecomePublisherQueryHandler : IRequestHandler<BecomePublisherQuery, string>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public BecomePublisherQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<string> Handle(BecomePublisherQuery request, CancellationToken cancellationToken)
    {
        await _userService.BecomePublisher(request.Username);
        return "Ok";
    }
}