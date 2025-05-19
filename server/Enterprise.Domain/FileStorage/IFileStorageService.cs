namespace Enterprise.Domain.FileStorage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(string fileName, string storagePath, byte[] fileData);
}
