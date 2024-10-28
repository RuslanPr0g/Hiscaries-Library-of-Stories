using HC.Application.Write.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Write.Stories.Command;

public class DeleteStoryAudioCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}