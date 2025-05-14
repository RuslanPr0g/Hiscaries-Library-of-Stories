using HC.PlatformUsers.Application.Read.PlatformUsers.ReadModels;

namespace HC.PlatformUsers.Application.Read.PlatformUsers.Queries.GetLibraryInfo;

public sealed class GetLibraryInfoQuery : IRequest<LibraryReadModel?>
{
    public Guid UserId { get; set; }
    public Guid? LibraryId { get; set; }
}