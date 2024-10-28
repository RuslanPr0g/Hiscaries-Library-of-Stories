using System.Threading.Tasks;

namespace HC.Application.FileStorage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(byte[] fileData, string fileName);
}
