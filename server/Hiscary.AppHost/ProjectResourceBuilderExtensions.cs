using Microsoft.Extensions.Configuration;

public static class ProjectResourceBuilderExtensions
{
    public static IResourceBuilder<T> WithJwtAndSaltSettings<T>(
        this IResourceBuilder<T> builder,
        IConfiguration configuration) where T : IResourceWithEnvironment
    {
        return builder.WithEnvironment("JwtSettings__Key", configuration["JwtSettings:Key"])
                      .WithEnvironment("JwtSettings__Issuer", configuration["JwtSettings:Issuer"])
                      .WithEnvironment("JwtSettings__Audience", configuration["JwtSettings:Audience"])
                      .WithEnvironment("JwtSettings__TokenLifeTime", configuration["JwtSettings:TokenLifeTime"])
                      .WithEnvironment("SaltSettings__StoredSalt", configuration["SaltSettings:StoredSalt"]);
    }
}