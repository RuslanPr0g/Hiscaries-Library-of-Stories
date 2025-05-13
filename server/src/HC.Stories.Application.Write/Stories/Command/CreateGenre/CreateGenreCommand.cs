namespace HC.Stories.Application.Write.Stories.Command.CreateGenre;

public sealed class CreateGenreCommand : IRequest<OperationResult>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] ImagePreview { get; set; }
}