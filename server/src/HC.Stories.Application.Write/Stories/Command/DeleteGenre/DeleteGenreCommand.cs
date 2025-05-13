namespace HC.Stories.Application.Write.Stories.Command.DeleteGenre;

public sealed class DeleteGenreCommand : IRequest<OperationResult>
{
    public Guid GenreId { get; set; }
}