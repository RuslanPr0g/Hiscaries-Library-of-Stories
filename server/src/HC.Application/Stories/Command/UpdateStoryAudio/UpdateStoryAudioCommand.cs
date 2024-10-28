using HC.Application.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Stories.Command;

public class UpdateStoryAudioCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public string Name { get; set; }
    public byte[] Audio { get; set; }
}