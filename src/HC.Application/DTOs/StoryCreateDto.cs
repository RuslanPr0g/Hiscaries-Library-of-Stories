using System;
using System.Collections.Generic;
using AutoMapper;
using HC.Application.Common.Mapping;
using HC.Domain.Story;

namespace HC.Application.DTOs;

public class StoryCreateDto : IMapWith<Story>
{
    public string StoryId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string AuthorName { get; set; }

    public List<int> GenreIds { get; set; }

    public int AgeLimit { get; set; }

    public DateTime DateWritten { get; set; }

    public string ImagePreview { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoryCreateDto, Story>();
    }
}