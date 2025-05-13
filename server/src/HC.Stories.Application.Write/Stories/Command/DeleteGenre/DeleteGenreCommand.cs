using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Stories.Command;

public sealed class DeleteGenreCommand : IRequest<OperationResult>
{
    public Guid GenreId { get; set; }
}