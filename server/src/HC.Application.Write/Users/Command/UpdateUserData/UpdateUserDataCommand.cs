﻿using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Users.Command;

public sealed class UpdateUserDataCommand : IRequest<OperationResult>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string UpdatedUsername { get; set; }
    public DateTime BirthDate { get; set; }

    public string PreviousPassword { get; set; }
    public string NewPassword { get; set; }
}