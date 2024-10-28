using HC.Application.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Stories.Command;

public class DeleteStoryCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}