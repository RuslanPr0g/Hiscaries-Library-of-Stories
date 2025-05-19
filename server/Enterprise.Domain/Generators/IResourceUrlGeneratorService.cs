namespace Enterprise.Domain.Generators;

public interface IResourceUrlGeneratorService
{
    string? GenerateImageUrlByFileName(string baseUrl, string fileName);
}
