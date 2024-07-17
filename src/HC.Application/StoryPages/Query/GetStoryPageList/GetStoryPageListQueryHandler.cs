using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HC.Application.DTOs;
using HC.Application.Interface;
using HC.Domain.Story;
using MediatR;

namespace HC.Application.StoryPages.Query.GetStoryPageList;

public class GetStoryPageListQueryHandler : IRequestHandler<GetStoryPageListQuery, List<StoryPageReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IStoryPageService _storyPageService;

    public GetStoryPageListQueryHandler(IStoryPageService storyPageService, IMapper mapper)
    {
        _storyPageService = storyPageService;
        _mapper = mapper;
    }

    public async Task<List<StoryPageReadDto>> Handle(GetStoryPageListQuery request, CancellationToken cancellationToken)
    {
        IList<StoryPage> storyPages = await _storyPageService.GetStoryPages(request.StoryId, request.User);

        List<StoryPageReadDto> mappedStoryPages = _mapper.Map<List<StoryPageReadDto>>(storyPages);

        return mappedStoryPages;
    }
}