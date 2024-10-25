using System;

namespace HC.Application.Interface.Generators;

public interface IResourceUrlGeneratorService
{
    string GenerateImageUrlByFileName(string filename);
}
