﻿using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Services;
using MediatR;

namespace HC.Application.Write.Stories.Command;

internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public UpdateCommentCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.UpdateComment(request);
    }
}