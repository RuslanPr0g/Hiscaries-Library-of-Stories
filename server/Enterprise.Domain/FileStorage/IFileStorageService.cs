namespace Enterprise.Domain.FileStorage;

public interface IFileStorageService
{
    Task<bool> SaveFileAsync(
        string fileName,
        string folderPath,
        byte[] fileData);
}
