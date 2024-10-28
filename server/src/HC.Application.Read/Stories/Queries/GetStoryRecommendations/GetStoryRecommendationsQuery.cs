﻿using HC.Application.Read.Stories.ReadModels;
using MediatR;
using System.Collections.Generic;

namespace HC.Application.Read.Stories.Queries;

public sealed class GetStoryRecommendationsQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public string Username { get; set; }
}