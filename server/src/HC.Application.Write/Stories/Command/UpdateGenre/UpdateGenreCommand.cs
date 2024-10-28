using HC.Application.Write.ResultModels.Response;
using MediatR;
using System;

namespace HC.Application.Write.Stories.Command;

public sealed class UpdateGenreCommand : IRequest<OperationResult>
{
    public Guid GenreId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] ImagePreview { get; set; }
}