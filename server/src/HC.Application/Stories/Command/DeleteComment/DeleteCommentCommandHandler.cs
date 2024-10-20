﻿using HC.Application.Interface;
using HC.Application.Models.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command.DeleteComment;

internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public DeleteCommentCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.DeleteComment(request);
    }
}