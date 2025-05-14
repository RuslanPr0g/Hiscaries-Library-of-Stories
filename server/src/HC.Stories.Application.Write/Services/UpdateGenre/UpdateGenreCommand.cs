namespace HC.Stories.Application.Write.Stories.Services.UpdateGenre;

public sealed class UpdateGenreCommand : IRequest<OperationResult>
{
    public Guid GenreId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] ImagePreview { get; set; }
}