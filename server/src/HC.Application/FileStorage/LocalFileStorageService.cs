using System.IO;
using System.Threading.Tasks;

namespace HC.Application.FileStorage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _baseDirectory;

    public LocalFileStorageService(string baseDirectory)
    {
        _baseDirectory = baseDirectory;
    }

    public async Task<string> SaveFileAsync(byte[] fileData, string fileName)
    {
        var directoryPath = Path.Combine(_baseDirectory, "images");
        var filePath = Path.Combine(directoryPath, fileName);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        await File.WriteAllBytesAsync(filePath, fileData);
        return fileName;
    }
}
