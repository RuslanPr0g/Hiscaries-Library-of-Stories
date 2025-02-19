﻿using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Users.Services;
using MediatR;

namespace HC.Application.Write.UserAccounts.Command.UpdateUserData;

public class UpdateUserDataCommandHandler : IRequestHandler<UpdateUserDataCommand, OperationResult>
{
    private readonly IUserAccountWriteService _userService;

    public UpdateUserDataCommandHandler(IUserAccountWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(UpdateUserDataCommand request, CancellationToken cancellationToken)
    {
        return await _userService.UpdateUserData(request);
    }
}