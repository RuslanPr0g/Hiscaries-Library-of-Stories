﻿using HC.Application.Read.Stories.ReadModels;
using HC.Application.Read.Stories.Services;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

public class GetStoryReadingHistoryQueryHandler : IRequestHandler<GetStoryReadingHistoryQuery, IEnumerable<StorySimpleReadModel>>
{
    private readonly IStoryReadService _storySevice;

    public GetStoryReadingHistoryQueryHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> Handle(GetStoryReadingHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetStoryReadingHistory(request);
    }
}