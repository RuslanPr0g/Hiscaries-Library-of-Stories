using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(byte[] fileData, string fileName);
}
