namespace HC.Application.DTOs;

public sealed class CreateAudioModel
{
    public string Name { get; set; }
    public byte[] Audio { get; set; }
}