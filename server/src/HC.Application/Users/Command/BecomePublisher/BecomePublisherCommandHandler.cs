﻿using System.Threading;
using System.Threading.Tasks;
using HC.Application.Common.Constants;
using HC.Application.Interface;
using HC.Application.Models.Response;
using MediatR;

namespace HC.Application.Users.Query.BecomePublisher;

public class BecomePublisherCommandHandler : IRequestHandler<BecomePublisherCommand, OperationResult>
{
    private readonly IUserWriteService _userService;

    public BecomePublisherCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(BecomePublisherCommand request, CancellationToken cancellationToken)
    {
        if (request.Username is null)
        {
            return OperationResult.CreateClientSideError(UserFriendlyMessages.UsernameEmpty);
        }

        await _userService.BecomePublisher(request.Username);
        return OperationResult.CreateSuccess();
    }
}