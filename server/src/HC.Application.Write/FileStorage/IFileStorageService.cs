namespace HC.Application.Write.FileStorage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(byte[] fileData, string fileName);
}
