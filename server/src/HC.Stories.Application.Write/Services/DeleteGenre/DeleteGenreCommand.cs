namespace HC.Stories.Application.Write.Stories.Services.DeleteGenre;

public sealed class DeleteGenreCommand : IRequest<OperationResult>
{
    public Guid GenreId { get; set; }
}