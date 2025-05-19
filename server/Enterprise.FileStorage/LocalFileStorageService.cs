using Enterprise.Domain.FileStorage;

namespace Enterprise.FileStorage;

public class LocalFileStorageService() : IFileStorageService
{
    public async Task<string> SaveFileAsync(string fileName, string storagePath, byte[] fileData)
    {
        var directoryPath = Path.Combine(storagePath, "images");
        var filePath = Path.Combine(directoryPath, fileName);

        Directory.CreateDirectory(directoryPath);

        await File.WriteAllBytesAsync(filePath, fileData);
        return fileName;
    }
}
