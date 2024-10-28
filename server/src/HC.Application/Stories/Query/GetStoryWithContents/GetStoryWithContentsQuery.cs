using HC.Application.Stories.ReadModels;
using MediatR;
using System;

namespace HC.Application.Stories.Query;

public sealed class GetStoryWithContentsQuery : IRequest<StoryWithContentsReadModel?>
{
    public Guid? Id { get; set; }
}