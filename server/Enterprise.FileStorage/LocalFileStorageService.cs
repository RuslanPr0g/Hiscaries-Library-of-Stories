using Enterprise.Domain.FileStorage;

namespace Enterprise.FileStorage;

public class LocalFileStorageService() : IFileStorageService
{
    public async Task<string> SaveFileAsync(
        string fileName,
        string relativeFolderPath,
        string rootStoragePath,
        byte[] fileData)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(relativeFolderPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootStoragePath);

        var normalizedRelativePath = relativeFolderPath
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar)
            .Trim(Path.DirectorySeparatorChar);

        var targetDirectory = Path.Combine(rootStoragePath, normalizedRelativePath);
        var filePath = Path.Combine(targetDirectory, fileName);

        Directory.CreateDirectory(targetDirectory);
        await File.WriteAllBytesAsync(filePath, fileData);

        return fileName;
    }
}
