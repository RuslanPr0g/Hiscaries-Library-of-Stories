using HC.Application.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Stories.Command;

public sealed class DeleteGenreCommand : IRequest<OperationResult>
{
    public Guid GenreId { get; set; }
}