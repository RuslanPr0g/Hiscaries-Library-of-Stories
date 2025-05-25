namespace Enterprise.Domain.FileStorage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(
        string fileName,
        string folderPath,
        byte[] fileData);
}
