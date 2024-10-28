using HC.Application.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Stories.Command;

public class BookmarkStoryCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid StoryId { get; set; }
}