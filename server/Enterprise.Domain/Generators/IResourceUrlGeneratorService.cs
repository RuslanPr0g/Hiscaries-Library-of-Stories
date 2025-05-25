namespace Enterprise.Domain.Generators;

public interface IResourceUrlGeneratorService
{
    string? GenerateImageUrlByFileName(
        string basePath,
        string relativePath,
        string fileName);
}
