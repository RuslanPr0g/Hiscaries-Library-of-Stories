﻿using Hiscary.Shared.Domain.Images;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Hiscary.Shared.Images.Compressors;

public sealed class DefaultImageCompressor : IImageCompressor
{
    public ValueTask<byte[]> CompressAsync(byte[] imagePreview, CompressionSettings settings)
    {
        using var image = Image.Load(imagePreview);
        int maxWidth = settings.MaxWidth;
        int maxHeight = maxWidth * settings.FormatHeight / settings.FormatWidth;

        if (image.Width > maxWidth || image.Height > maxHeight)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(maxWidth, maxHeight)
            }));
        }

        using var ms = new MemoryStream();
        image.Save(ms, new JpegEncoder { Quality = 75 });
        return ValueTask.FromResult(ms.ToArray());
    }
}
