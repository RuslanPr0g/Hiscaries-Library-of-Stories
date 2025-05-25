using Enterprise.Domain.FileStorage;
using HC.Media.Domain;
using Microsoft.Extensions.Options;

namespace HC.Media.FileStorage;

public class LocalFileStorageService(
    IOptions<ResourceSettings> options,
    ResourceSettings settings) : IFileStorageService
{
    private readonly string _storagePath = options.Value.StoragePath ?? settings.StoragePath;

    public async Task<bool> SaveFileAsync(
        string fileName,
        string folderPath,
        byte[] fileData)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(folderPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(_storagePath);

        if (fileData.Length == 0)
        {
            throw new ArgumentException($"{nameof(fileData)} argument cannot be empty.");
        }

        var normilizedDirectory = folderPath
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar)
            .Trim(Path.DirectorySeparatorChar);

        var targetDirectory = Path.Combine(_storagePath, normilizedDirectory);

        var filePath = Path.Combine(targetDirectory, fileName);

        Directory.CreateDirectory(targetDirectory);
        await File.WriteAllBytesAsync(filePath, fileData);

        return true;
    }
}
