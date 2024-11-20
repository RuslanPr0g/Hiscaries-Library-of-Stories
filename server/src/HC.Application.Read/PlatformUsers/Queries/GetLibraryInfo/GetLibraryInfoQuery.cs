using HC.Application.Read.Users.ReadModels;
using MediatR;

namespace HC.Application.Read.Users.Queries;

public sealed class GetLibraryInfoQuery : IRequest<LibraryReadModel?>
{
    public Guid UserId { get; set; }
    public Guid? LibraryId { get; set; }
}