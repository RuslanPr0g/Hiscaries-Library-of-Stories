﻿using HC.Domain.Stories;

namespace HC.Application.Read.Genres.ReadModels;

public sealed class GenreReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] ImagePreview { get; set; }

    public static GenreReadModel FromDomainModel(Genre genre)
    {
        return new GenreReadModel
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description,
            ImagePreview = genre.ImagePreview
        };
    }
}
