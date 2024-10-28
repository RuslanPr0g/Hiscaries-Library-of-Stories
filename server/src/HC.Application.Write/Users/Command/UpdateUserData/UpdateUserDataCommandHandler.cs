﻿using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Write.Users.Command;

public class UpdateUserDataCommandHandler : IRequestHandler<UpdateUserDataCommand, OperationResult>
{
    private readonly IUserWriteService _userService;

    public UpdateUserDataCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        return await _userService.UpdateUserData(request);
    }
}