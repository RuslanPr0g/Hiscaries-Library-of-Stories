using HC.Application.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Stories.Command;

public class StoryScoreCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid UserId { get; set; }
    public int Score { get; set; }
}