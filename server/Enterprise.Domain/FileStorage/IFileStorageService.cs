namespace Enterprise.Domain.FileStorage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(byte[] fileData, string fileName);
}
