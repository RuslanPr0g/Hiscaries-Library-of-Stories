﻿using HC.Application.Common.Constants;
using HC.Application.Interface;
using HC.Application.Models.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Users.Command.PublishReview;

public class PublishReviewCommandHandler : IRequestHandler<PublishReviewCommand, OperationResult>
{
    private readonly IUserWriteService _userService;

    public PublishReviewCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(PublishReviewCommand request, CancellationToken cancellationToken)
    {
        if (request.Message is null)
        {
            return OperationResult.CreateClientSideError(UserFriendlyMessages.ReviewMessageCannotBeEmpty);
        }

        return await _userService.PublishReview(request);
    }
}