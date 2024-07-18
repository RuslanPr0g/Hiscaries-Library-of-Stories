using HC.Application.Models.Response;
using MediatR;
using System;

namespace HC.Application.Stories.Command.DeleteStory;

public class DeleteStoryCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}