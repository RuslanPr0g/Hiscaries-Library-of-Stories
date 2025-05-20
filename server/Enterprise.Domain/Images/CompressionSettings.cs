namespace Enterprise.Domain.Images;

public sealed class CompressionSettings
{
    public int FormatWidth { get; set; } = 16;
    public int FormatHeight { get; set; } = 9;
    public int MaxWidth { get; set; } = 1080;

    public static CompressionSettings Default = new();
}