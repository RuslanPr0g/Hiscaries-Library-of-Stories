﻿using HC.Application.Read.Stories.ReadModels;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

public sealed class GetStoryWithContentsQuery : IRequest<StoryWithContentsReadModel?>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}