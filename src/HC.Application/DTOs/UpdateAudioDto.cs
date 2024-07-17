namespace HC.Application.DTOs;

public sealed class UpdateAudioDto
{
    public int AudioId { get; set; }
    public byte[] Audio { get; set; }
    public string Name { get; set; }
}