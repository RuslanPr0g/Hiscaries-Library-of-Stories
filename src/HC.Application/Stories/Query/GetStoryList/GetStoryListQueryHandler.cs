using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.DTOs;
using HC.Application.Interface;
using MediatR;

namespace HC.Application.Stories.Query;

public class GetStoryListQueryHandler : IRequestHandler<GetStoryListQuery, List<StoryReadDto>>
{
    private readonly IStoryWriteService _storySevice;

    public GetStoryListQueryHandler(IStoryWriteService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<List<StoryReadDto>> Handle(GetStoryListQuery request, CancellationToken cancellationToken)
    {
        List<StoryReadDto> stories = await _storySevice.GetAllStories(request.User);

        if (request.All) return stories;

        int? idRequest = request.Id;
        string searchRequest = request.Search;
        string genreRequest = request.Genre;

        if (idRequest is not null && idRequest > 0)
            return new List<StoryReadDto> { stories.FirstOrDefault(s => s.Id == idRequest) };

        if (!string.IsNullOrEmpty(searchRequest))
            stories = stories.Where(s =>
                s.Title.Contains(searchRequest, StringComparison.InvariantCultureIgnoreCase) ||
                s.Description.Contains(searchRequest, StringComparison.InvariantCultureIgnoreCase)).ToList();


        if (!string.IsNullOrEmpty(genreRequest))
        {
            List<StoryReadDto> sttoriesWithGenres = new List<StoryReadDto>();

            foreach (StoryReadDto story in stories)
            {
                bool isAny = story.Genres.Any(x =>
                    x.Name.Contains(genreRequest, StringComparison.InvariantCultureIgnoreCase)
                    || x.Description.Contains(genreRequest, StringComparison.InvariantCultureIgnoreCase));

                if (isAny) sttoriesWithGenres.Add(story);
            }

            stories = sttoriesWithGenres;
        }

        return stories;
    }
}